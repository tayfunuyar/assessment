using ContactService.Entities;

namespace ContactService.Dtos {
    public class ContactInformationCreateDto {
        public ContactInformationType ContactInformationType { get;set;}
        public string  Information { get; set; }
    }
}