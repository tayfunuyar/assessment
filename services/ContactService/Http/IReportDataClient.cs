using System;
using System.Threading.Tasks;
using ContactService.Dtos;
using ContactService.Models;

namespace ContactService.Http {
    public interface IReportDataClient
    {
          Task<GenericResponse<Guid>> SendReportRequest(ReportRequestDto reportRequestDto);
    }
}