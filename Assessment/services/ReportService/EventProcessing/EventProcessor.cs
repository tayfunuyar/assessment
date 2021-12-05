using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OfficeOpenXml;
using ReportService.Data.Abstract;
using ReportService.Dtos;
using ReportService.Http;

namespace ReportService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;

        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);
            switch (eventType)
            {
                case EventType.ReportRequestPublished:
                    prepareReport(message);
                    break;
                default:
                    break;
            }
        }
        private EventType DetermineEvent(string notificationMessage)
        {
            var eventType = JsonConvert.DeserializeObject<GenericEventDto>(notificationMessage);
            Console.WriteLine("Report Request Income...");
            switch (eventType.Event)
            {
                case "ReportRequest_Publish":
                    return EventType.ReportRequestPublished;
                default:
                    return EventType.Undetermined;
            }
        }

        private  void  prepareReport(string reportMessage)
        {
            List<LocationResultDto> locationResultList = new List<LocationResultDto>();
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IReportRepository>();
                var contactDataClient = scope.ServiceProvider.GetRequiredService<IContactDataClient>();
                var reportRequestDto = JsonConvert.DeserializeObject<ReportPublishedDto>(reportMessage);
                try
                {
                    var report = repo.GetById(reportRequestDto.ReportUuid).Result;
                    var contactInformationList = contactDataClient.GetContactInformations().Result;
                    var locations = contactInformationList.GroupBy(x => new { x.ContactInformationType, x.Information })
                    .Where(x => x.Key.ContactInformationType == Entities.ContactInformationType.Location).Select(x => x.Key.Information);
                    foreach (var location in locations)
                    {
                        LocationResultDto locationResultDto = new LocationResultDto();
                        locationResultDto.Location = location;
                        locationResultDto.ContactCount = contactInformationList.Where(x => x.ContactInformationType == Entities.ContactInformationType.Location
                         && x.Information == location).GroupBy(x => x.ContactUuid).Count();
                        var locationPersonUUidList = contactInformationList.Where(x => x.ContactInformationType == Entities.ContactInformationType.Location
                       && x.Information == location).GroupBy(x => x.ContactUuid).Select(x => x.Key).ToList();
                        locationResultDto.ContactNumberContact = contactInformationList.Where(x => x.ContactInformationType == Entities.ContactInformationType.PhoneNumber && locationPersonUUidList.Contains(x.ContactUuid)).Count();
                        locationResultList.Add(locationResultDto);
                    }

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "ReportFiles");
                    string sFileName = $@"LocationReport-{reportRequestDto.ReportUuid}.xlsx";
                    FileInfo file = new FileInfo(Path.Combine(path, sFileName));
                    if (file.Exists)
                    {
                        file.Delete();
                        file = new FileInfo(Path.Combine(path, sFileName));
                    }
                    using (ExcelPackage package = new ExcelPackage(file))
                    {

                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("LocationReport");
                        worksheet.Cells[1, 1].Value = "Location";
                        worksheet.Cells[1, 2].Value = "ContactCount";
                        worksheet.Cells[1, 3].Value = "ContactNumberContact";

                        for (int i = 0; i < locationResultList.Count; i++)
                        {
                            worksheet.Cells[$"A{i+2}"].Value =locationResultList[i].Location;
                            worksheet.Cells[$"B{i+2}"].Value = locationResultList[i].ContactCount;
                            worksheet.Cells[$"C{i+2}"].Value = locationResultList[i].ContactNumberContact;
                        }
                        package.Save();
                    }
                    report.ReportStatus = Entities.ReportStatusType.Completed;
                    Console.WriteLine(report.Uuid);
                    repo.Update(report).Wait();
                }
                catch (Exception ex)
                {
                     Console.WriteLine(ex.Message);
                }
            }
        }


        enum EventType
        {
            ReportRequestPublished,
            Undetermined
        }
    }
}