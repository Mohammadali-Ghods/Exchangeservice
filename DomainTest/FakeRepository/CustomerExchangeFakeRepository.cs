using Domain.Interfaces.Base;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTest.FakeRepository
{
    public class CustomerExchangeFakeRepository : ICustomerExchangeRepository
    {
        private readonly List<CustomerExchange> customerExchanges = new List<CustomerExchange>();

        public async Task Delete(object id)
        {
            customerExchanges.RemoveAll(x => x.ID == id);
        }

        public IEnumerable<CustomerExchange> GetAll()
        {
            return customerExchanges;
        }

        public List<CustomerExchange> GetAllByCustomerID(string customerid)
        {
            return customerExchanges.Where(x => x.CustomerID == customerid).ToList();
        }

        public List<CustomerExchange> GetAllByCustomerIDInLastHour(string customerid)
        {
            return customerExchanges.Where(x => x.CustomerID == customerid &&
            x.Status.Count == 2 && x.TransactionDate.AddHours(1) > DateTime.Now).ToList();
        }

        public async Task<CustomerExchange> GetById(object id)
        {
            return customerExchanges.Where(x => x.ID == id).FirstOrDefault();
        }

        public async Task<CustomerExchange> GetByIdWithStatus(string id)
        {
            return customerExchanges.Where(x => x.ID == id).FirstOrDefault();
        }

        public async Task Insert(CustomerExchange obj)
        {
            customerExchanges.Add(obj);
        }

        public async Task Save()
        {
            return;
        }

        public void Update(CustomerExchange obj)
        {
            var updatedvalue = customerExchanges.FirstOrDefault(x => x.ID == obj.ID);
            if (obj != null) updatedvalue = obj;
        }
    }
}
