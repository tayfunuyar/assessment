using System;
using System.Collections.Generic;
using System.Linq;
using ContactService.Data.Abstract;
using ContactService.Dtos;
using ContactService.Entities;

namespace ContactService.Data.Concrete {
    public class ContactInformationRepository : GenericRepository<ContactInformation>, IContactInformationRepository
    {
        public ContactInformationRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<ContactInformation> GetContactInformationByContactId(Guid Uuid)
        {
              return GetAll().Where(x=>x.ContactUuid == Uuid).ToList();
        }
    }
}