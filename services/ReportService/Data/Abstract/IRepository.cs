using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReportService.Data.Concrete;

namespace ReportService.Data.Abstract
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        Task<T> GetById(Guid id);
        Task<Guid> Insert(T entity);
        bool IsExist (Guid id);
        
        Task Update(T entity);
        Task Delete(Guid id);
    }
}