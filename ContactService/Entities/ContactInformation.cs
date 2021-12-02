using System;
using System.ComponentModel.DataAnnotations;
using ContactService.Data.Concrete;

namespace ContactService.Entities {
     public class ContactInformation : BaseEntity {
        
         public ContactInformationType  ContactInformationType { get; set; }
         public string  Information { get; set; } 
         public Guid ContactUuid { get; set; }
         public Contact Contact { get;set;} 
     }
}