using System;
using System.Text.Json.Serialization;
using ContactService.Entities;

namespace ContactService.Dtos {
    public class ContactInformationDto {
        public Guid Uuid { get; set; }
        public ContactInformationType ContactInformationType { get;set;}
        [JsonIgnore]
        public Guid ContactUuid { get; set; }
        public string  Information { get; set; }
    }
}