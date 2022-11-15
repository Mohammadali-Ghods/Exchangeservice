using Domain.Commands.CustomerExchangeCommands.Validations;
using Domain.Interfaces.Base;
using Domain.ResultModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Interfaces;

namespace Domain.Commands.CustomerExchangeCommands
{
    public class CallUpCommand : Base.CustomerExchangeCommand,
        IRequestHandler<CallUpCommand, Result>

    {
        private ICustomerExchangeRepository _customerExchangeRepository;
        private ICacheManagement<CustomerExchange> _cacheManagement;
        public CallUpCommand(ICustomerExchangeRepository customerExchangeRepository,
              ICacheManagement<CustomerExchange> cacheManagement)
        {
            _customerExchangeRepository = customerExchangeRepository;
            _cacheManagement = cacheManagement;
        }
        public CallUpCommand()
        {
        }
        public async Task<Result> Handle(CallUpCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return new Result() { FailedResults = request.ValidationResult };

            var customerexchange = new CustomerExchange(request.ID,
                request.CustomerID, request.FromExchange, request.ToExchange, request.Amount,
                request.ExchangeRate, request.ConvertedValue);

            await _customerExchangeRepository.Insert(customerexchange);
            _cacheManagement.SetSingleCache(request.ID, customerexchange, 4);

            await _customerExchangeRepository.Save();

            return new Result();
        }
        public bool IsValid()
        {
            ValidationResult = new CallUpCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
