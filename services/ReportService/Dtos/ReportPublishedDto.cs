using System;

namespace ReportService.Dtos
{
    public class ReportPublishedDto
    { 
        public Guid ReportUuid { get; set; }
        public string Event { get; set; }
    }
}