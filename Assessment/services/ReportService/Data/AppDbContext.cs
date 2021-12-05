using ReportService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ReportService.Data {
    public class AppDbContext: DbContext {
       public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
       {
           
       }

       public DbSet<Report> Reports { get;set;}

       protected override void OnModelCreating(ModelBuilder builder){

            builder.HasPostgresExtension("uuid-ossp");  
       }
    }
}