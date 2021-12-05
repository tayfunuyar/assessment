using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactService.Data.Abstract;
using ContactService.Dtos;
using ContactService.Entities;

namespace ContactService.Data.Concrete
{
    public class ContactInformationRepository : GenericRepository<ContactInformation>, IContactInformationRepository
    {
        public ContactInformationRepository(AppDbContext context) : base(context)
        {
        }

        public Task DeleteContactInformationByContactId(Guid Uuid)
        {
            var allListByContact = GetAll().Where(x => x.ContactUuid == Uuid);
            return Delete(allListByContact.ToList());
        }

        public IEnumerable<ContactInformation> GetContactInformationByContactId(Guid Uuid)
        {
            return GetAll().Where(x => x.ContactUuid == Uuid).ToList();
        }
    }
}