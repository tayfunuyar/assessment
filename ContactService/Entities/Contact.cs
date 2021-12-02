using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactService.Entities
{
    public class Contact
    {
        [Key]
        [Required]
        public Guid Uuid { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Firm { get; set; }
        public List<ContactInformation> ContactInformations { get; set; } = new List<ContactInformation>();

    }
}