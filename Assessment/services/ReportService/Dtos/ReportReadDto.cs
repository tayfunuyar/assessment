using System;
using ReportService.Entities;

namespace ReportService.Dtos
{
    public class ReportReadDto
    {
        public string Uuid { get; set; }
        public DateTime Date { get; set; }
        public ReportStatusType ReportStatus { get; set; }

        public string ReportStatusText
        {
            get
            {
                return ReportStatus.ToString();
            }
        }
    }
}