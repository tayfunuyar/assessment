using ReportService.Dtos;

namespace ReportService.AsyncDataServices {
    public interface IMessageBusClient {
         void PublishNewReportRequest(ReportPublishedDto reportPublishedDto);
    } 
}