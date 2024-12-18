using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(Guid id);
        void Add(T entity);
        bool Update(T entity, Guid id);
        bool Delete(Guid id);
    }
}
