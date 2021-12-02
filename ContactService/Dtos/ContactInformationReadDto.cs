using System;
using ContactService.Entities;

namespace ContactService.Dtos
{
    public class ContactInformationReadDto
    {
        public Guid Uuid{ get;set;}
        public ContactInformationType ContactInformationType { get; set; }
        public string Information { get; set; }
        public string InformationType
        {
            get
            {
                return ContactInformationType.ToString();
            }
        }
    }
}