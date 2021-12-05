using System;
using ReportService.Entities;

namespace ReportService.Dtos
{
    public class ReportRequestDto
    {
        public ReportStatusType Status { get; set; }
        public DateTime Date { get; set; }
    }
}