using System;
using ReportService.Entities;

namespace ReportService.Dtos {
    public class ContactInformationReadDto {
         public Guid Uuid{ get;set;}
        public ContactInformationType ContactInformationType { get; set; }
        public string Information { get; set; }
         public Guid ContactUuid { get; set; }
        public string InformationType
        {
            get
            {
                return ContactInformationType.ToString();
            }
        }
        
    }
}