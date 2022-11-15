using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(object id);
        Task Insert(T obj);
        void Update(T obj);
        Task Delete(object id);
        Task Save();
    }
}
