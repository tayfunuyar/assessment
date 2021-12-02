using System;
using System.Linq;
using System.Threading.Tasks;
using ContactService.Data.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Data.Concrete {
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
             return _context.Set<T>();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x=>x.Uuid== id);
        }

        public async Task Insert(T entity)
        {
             await _context.Set<T>().AddAsync(entity);
             await _context.SaveChangesAsync();
        }

        public bool IsExist(Guid id)
        {
             return _context.Set<T>().Any(x=>x.Uuid == id); 
        }

        public async Task Update(T entity)
        {
              _context.Set<T>().Update(entity);
             await _context.SaveChangesAsync();
        }
    }
}