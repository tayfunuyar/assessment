using ContactService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Data {
    public class AppDbContext: DbContext {
       public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
       {
           
       }
       public DbSet<Contact> Contacts{ get; set; }
       public DbSet<ContactInformation> ContactInformations { get;set;}

       protected override void OnModelCreating(ModelBuilder builder){
             
       }
    }
}