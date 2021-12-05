using System;
using ReportService.Data.Concrete;

namespace ReportService.Entities {
    public class Report : BaseEntity{
        public DateTime  Date { get; set; }
        public ReportStatusType ReportStatus { get; set; }
    } 
}