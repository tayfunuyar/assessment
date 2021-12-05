using System;
using System.Threading.Tasks;
using AutoMapper; 
using ContactService.Dtos;
using ContactService.Http;
using ContactService.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportRequestController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReportDataClient _reportDataClient;

        public ReportRequestController(IMapper mapper, IReportDataClient reportDataClient)
        {
            _reportDataClient = reportDataClient;
            _mapper = mapper;
        }
        [HttpGet(Name = "ReportRequestByLocation")]
        public async Task<IActionResult> ReportRequestByLocation()
        {
            try
            {
                var reportResponse = await _reportDataClient.SendReportRequest(new ReportRequestDto{
                         Date = DateTime.Now,
                         Status = ReportStatusType.Preparing
                });
                 Console.WriteLine(reportResponse.Response);
               return Ok(reportResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
                return Ok(GenericResponse<Guid>.ErrorResponse(ex.Message));
            } 
            
        }
    }
}