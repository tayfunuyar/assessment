using System;
using System.Collections.Generic;
using ContactService.Dtos;
using ContactService.Entities;

namespace ContactService.Data.Abstract { 
    public interface  IContactInformationRepository: IRepository<ContactInformation> {
        
         IEnumerable<ContactInformation> GetContactInformationByContactId(Guid Uuid);
    }
}