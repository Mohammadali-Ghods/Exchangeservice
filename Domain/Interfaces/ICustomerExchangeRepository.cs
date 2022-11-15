using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Base
{
    public interface ICustomerExchangeRepository : IRepository<CustomerExchange>
    {
        public List<CustomerExchange> GetAllByCustomerID(string customerid);
        public List<CustomerExchange> GetAllByCustomerIDInLastHour(string customerid);
        public Task<CustomerExchange> GetByIdWithStatus(string id);
    }
}