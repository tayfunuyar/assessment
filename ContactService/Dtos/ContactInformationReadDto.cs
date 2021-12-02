using ContactService.Entities;

namespace ContactService.Dtos
{
    public class ContactInformationReadDto
    {
        public ContactInformationType ContactInformationType { get; set; }
        public string Information { get; set; }
    }
}