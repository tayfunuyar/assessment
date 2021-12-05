using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReportService.AsyncDataServices;
using ReportService.Data.Abstract;
using ReportService.Dtos;
using ReportService.Entities;
using ReportService.Models;

namespace ReportService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBusClient;
        private readonly IReportRepository _reportRepository;
        public ReportController(IMapper mapper, IMessageBusClient messageBusClient, IReportRepository reportRepository)
        {
            _mapper = mapper;
            _messageBusClient = messageBusClient;
            _reportRepository = reportRepository;
        }

        [HttpGet]
        public ActionResult<List<ReportReadDto>> GetReports()
        {
            var reports = _reportRepository.GetAll().ToList();
            return Ok(_mapper.Map<List<ReportReadDto>>(reports));
        }
        [HttpGet("{uuid}")]
        public async Task<ActionResult> DownloadReport(Guid uuid)
        {
            
            var path = Path.Combine(Directory.GetCurrentDirectory(), "ReportFiles");
            string sFileName = $@"LocationReport-{uuid}.xlsx";
            FileInfo file = new FileInfo(Path.Combine(path, sFileName));
            if (file.Exists)
            { 
                file = new FileInfo(Path.Combine(path, sFileName));

                FileStream fileStream = new FileStream(file.FullName, FileMode.Open);
                
                fileStream.Position = 0;
                var contentType = "application/octet-stream";

                return File(fileStream, contentType, sFileName);
            }
            else
            {
                 return NotFound();
            }

        }


        [HttpPost]
        public async Task<ActionResult<GenericResponse<Guid>>> CreateReportRequest(ReportRequestDto reportRequestDto)
        {

            try
            {
                var reportRequest = _mapper.Map<Report>(reportRequestDto);
                var reportUuid = await _reportRepository.Insert(reportRequest);
                Console.WriteLine(reportUuid);
                _messageBusClient.PublishNewReportRequest(new ReportPublishedDto
                {
                    ReportUuid = reportUuid,
                    Event = "ReportRequest_Publish"
                });

                return Ok(
                     GenericResponse<string>.SuccessResponse(ApiResponseMessage.ReportRequestCompleted, reportUuid.ToString())
                );
            }
            catch (Exception ex)
            {
                return Ok(
                    GenericResponse<string>.ErrorResponse(ApiResponseMessage.ReportRequestCompleted + ex.Message)
               );
            }

        }


    }
}