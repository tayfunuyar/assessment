using System;

namespace ContactService.Dtos {
    public class ContactReadDto {
        public Guid Uuid { get; set; }
        public string Name { get; set; } 
        public string Surname { get; set; }
        public string Firm { get; set; }
    }
}