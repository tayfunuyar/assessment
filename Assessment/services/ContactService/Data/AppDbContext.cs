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

            builder.HasPostgresExtension("uuid-ossp");
            
              builder.Entity<Contact>()
              .HasMany(x=>x.ContactInformations)
              .WithOne(x=>x.Contact!)
              .HasForeignKey(x=>x.ContactUuid); 


              builder.Entity<ContactInformation>()
              .HasOne(x=>x.Contact)
              .WithMany(x=>x.ContactInformations)
              .HasForeignKey(x=>x.ContactUuid);
       }
    }
}