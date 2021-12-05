using System;
using System.ComponentModel.DataAnnotations;

namespace ContactService.Dtos
{
    public class ContactDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Firm { get; set; }
    }
}