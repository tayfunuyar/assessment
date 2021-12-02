using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ContactService.Data.Concrete;

namespace ContactService.Entities
{
    public class Contact:BaseEntity
    {
      
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Firm { get; set; }
        public List<ContactInformation> ContactInformations { get; set; } = new List<ContactInformation>();

    }
}