using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTest.FakeCache
{
    public class FakeCacheManagement : ICacheManagement<CustomerExchange>
    {
        private List<CustomerExchange> Items;
        private CustomerExchange Item;
        public async Task<IEnumerable<CustomerExchange>> GetListCache(string cacheKey)
        {
            return Items;
        }

        public async Task<CustomerExchange> GetSingleCache(string cacheKey)
        {
            return Item;
        }

        public void SetListCache(string cacheKey, List<CustomerExchange> t, int minutes)
        {
            Items = t;
        }

        public void SetSingleCache(string cacheKey, CustomerExchange t, int minutes)
        {
            Item = t;
        }
    }
}
