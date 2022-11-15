using Domain.Interfaces.Base;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class CustomerExchangeRepository : Repository<CustomerExchange>,
        ICustomerExchangeRepository
    {
        public List<CustomerExchange> GetAllByCustomerID(string customerid)
        {
            return table.Include(x => x.Status).Where(x => x.CustomerID == customerid
            && x.Status.Count==2).ToList();
        }

        public List<CustomerExchange> GetAllByCustomerIDInLastHour(string customerid)
        {
            return table.Include(x => x.Status).Where(x => x.CustomerID == customerid &&
              x.TransactionDate.AddHours(1) > DateTime.Now).ToList();
        }
        public async Task<CustomerExchange> GetByIdWithStatus(string id)
        {
            return await table.Include(x => x.Status).Where(x => x.ID == id).FirstOrDefaultAsync();
        }
    }
}
