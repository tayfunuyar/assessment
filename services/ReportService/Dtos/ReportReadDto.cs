using ReportService.Entities;

namespace ReportService.Dtos
{
    public class ReportReadDto
    {
        public string Uuid { get; set; }
        public ReportStatusType ReportStatusType { get; set; }

        public string ReportStatus
        {
            get
            {
                return ReportStatusType.ToString();
            }
        }
    }
}