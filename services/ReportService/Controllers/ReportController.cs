using System;
using System.Collections.Generic;
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
        public ActionResult<IEnumerable<ReportReadDto>> GetReports()
        {
            var reports = _reportRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<ReportReadDto>>(reports));
        }

        [HttpPost]
        public async Task<ActionResult<GenericResponse<Guid>>> CreateReportRequest(ReportRequestDto reportRequestDto)
        {

            try
            {
                var reportRequest = _mapper.Map<Report>(reportRequestDto);
                var reportUuid = await _reportRepository.Insert(reportRequest);
                _messageBusClient.PublishNewReportRequest(new ReportPublishedDto
                {
                    ReportUuid = reportUuid,
                    Event = "ReportRequest_Publish"
                });

                return Ok(
                     GenericResponse<Guid>.SuccessResponse(ApiResponseMessage.ReportRequestCompleted, reportUuid)
                );
            }
            catch (Exception ex)
            {
                return Ok(
                    GenericResponse<Guid>.ErrorResponse(ApiResponseMessage.ReportRequestCompleted + ex.Message)
               );
            } 

        }


    }
}