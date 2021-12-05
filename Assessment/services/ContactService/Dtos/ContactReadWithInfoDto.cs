using System.Collections.Generic;

namespace ContactService.Dtos {
    public class ContactReadWithInfoDto {
          public ContactReadDto Contact { get; set; }
          public List<ContactInformationReadDto> ContactInfoList { get;set;}
    }
}