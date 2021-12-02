using System;
using System.ComponentModel.DataAnnotations;

namespace ContactService.Entities {
     public class ContactInformation {
         [Key]
         [Required]
         public Guid Uuid { get; set; }
         public ContactInformationType  ContactInformationType { get; set; }
         public string  Information { get; set; } 
         public Guid ContactUuid { get; set; }
         public Contact Contact { get;set;}
     }
}