using ContactService.Data.Abstract;
using ContactService.Entities;

namespace ContactService.Data.Concrete {
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(AppDbContext context) : base(context)
        {
        }
    }
}