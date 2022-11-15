using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICacheManagement<T>
    {
        public Task<IEnumerable<T>> GetListCache(string cacheKey);
        public Task<T> GetSingleCache(string cacheKey);
        public void SetSingleCache(string cacheKey, T t, int minutes);
        public void SetListCache(string cacheKey, List<T> t, int minutes);

    }
}
