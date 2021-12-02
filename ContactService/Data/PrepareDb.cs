using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContactService.Data
{
    public static class PrepareDb
    {
        public static void PreparePopulation(IApplicationBuilder app, bool prod)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), prod);
            }
        }
        private static void SeedData(AppDbContext context, bool prod)
        {

            try
            {
                context.Database.Migrate();
                if (!context.Contacts.Any())
                {
                    context.Contacts.AddRange(
                        new Entities.Contact()
                        {
                            Firm = "Setur Antalya",
                            Name = "Tayfun",
                            Surname = "Uyar",
                            ContactInformations = {
                              new Entities.ContactInformation(){ContactInformationType= Entities.ContactInformationType.Email, Information="tayfunuyar06@gmail.com"},
                              new Entities.ContactInformation(){ContactInformationType= Entities.ContactInformationType.Location, Information="Ankara"},
                              new Entities.ContactInformation(){ContactInformationType= Entities.ContactInformationType.PhoneNumber, Information="05468158650"}
                        }
                        },
                        new Entities.Contact()
                        {
                            Firm = "Setur Ankara",
                            Name = "Tufan",
                            Surname = "Uyar",
                            ContactInformations = {
                             new Entities.ContactInformation(){ContactInformationType= Entities.ContactInformationType.Email, Information="tufanuyar06@gmail.com"},
                             new Entities.ContactInformation(){ContactInformationType= Entities.ContactInformationType.Location, Information="Ankara"},
                             new Entities.ContactInformation(){ContactInformationType= Entities.ContactInformationType.PhoneNumber, Information="05355865908"}
                        }
                        }
                    );
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("We have already data...");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not run migrations: {ex.Message}");
            }



        }

    }
}
