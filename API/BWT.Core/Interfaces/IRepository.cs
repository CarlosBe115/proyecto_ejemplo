using BWT.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BWT.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(int Id);
        Task Add(T entity);
        void Update(T entity);
        Task Delete(int Id);
    }
}
