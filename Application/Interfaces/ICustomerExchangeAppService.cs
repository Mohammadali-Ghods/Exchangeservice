using Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICustomerExchangeAppService : IDisposable
    {
        public IEnumerable<CustomerExchangeViewModel> GetAll();
        public Task<CustomerExchangeViewModel> GetById(string transactionid);
        public Task<CustomerExchangeViewModel> GetByTransaction(string transactionid,string customerid);
        public IEnumerable<CustomerExchangeViewModel> GetByCustomer(string customerid);
        public Task<ResultModel> CallUp(CallUpViewModel commandmodel,string customerid);
        public Task<ResultModel> FinalizeTransaction(FinalizeTransactionViewModel commandmodel,
            string customerid);
    }
}
