using System.Collections.Generic;

namespace ContactService.Dtos{
    public class ReportPublishedDto {
      
        public string  Location { get; set; }
        public List<ContactInformationReadDto> ContactInformations{ get; set; }
        public string  Event { get; set; }
    }
}