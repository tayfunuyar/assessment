using ReportService.Data.Abstract;
using ReportService.Entities;

namespace ReportService.Data.Concrete {
    public class ReportRepository : GenericRepository<Report>, IReportRepository
    {
        public ReportRepository(AppDbContext context) : base(context)
        {
        }
    }
}