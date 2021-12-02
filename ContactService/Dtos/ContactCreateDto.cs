using System.ComponentModel.DataAnnotations;

namespace ContactService.Dtos {
    public class ContactCreateDto{
       
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Firm { get; set; }
    }
}