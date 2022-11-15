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
using FluentValidation.Results;
using Domain.ValueObject;
using Domain.Commands.CustomerExchangeCommands.Base;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.CustomerExchangeCommands
{
    public class FinalizeTransactionCommand : CustomerExchangeCommand,
      IRequestHandler<FinalizeTransactionCommand, Result>

    {
        private ICustomerExchangeRepository _customerExchangeRepository;
        private ICacheManagement<CustomerExchange> _cacheManagement;
        private ILogger<FinalizeTransactionCommand> _logger;
        public FinalizeTransactionCommand(ICustomerExchangeRepository customerExchangeRepository,
              ICacheManagement<CustomerExchange> cacheManagement,
              ILogger<FinalizeTransactionCommand> logger
              )
        {
            _customerExchangeRepository = customerExchangeRepository;
            _cacheManagement = cacheManagement;
            _logger = logger;
        }
        public FinalizeTransactionCommand()
        {
        }
        public async Task<Result> Handle(FinalizeTransactionCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return new Result() { FailedResults = request.ValidationResult };

            var transaction = await _cacheManagement.GetSingleCache(request.ID);

            if (transaction == null) 
            {
                _logger.Log(LogLevel.Warning, $"No transaction found for transaction" +
                    $" id {request.ID} in cache maybe we have some problem in caching system");
                await _customerExchangeRepository.GetByIdWithStatus(request.ID);
            }

            if (transaction == null)
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("TransactionNotFound"
                    + Guid.NewGuid().ToString(), "Transaction not found"));
                return new Result() { FailedResults = request.ValidationResult };
            }

            if (transaction.Status.Where(x => x.Type == StatusTypes.ExchangeCompleted).Count() != 0)
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("TransactionCompletedBefore"
                   + Guid.NewGuid().ToString(), "Transaction Completed Before"));
                return new Result() { FailedResults = request.ValidationResult };
            }

            if (transaction.Status.Where(x => x.Type == StatusTypes.CallUpFinished)
                .FirstOrDefault().StatusDate.AddMinutes(30) < DateTime.Now)
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("TransactionExpired"
                  + Guid.NewGuid().ToString(), "Transaction expired please open another transaction"));
                return new Result() { FailedResults = request.ValidationResult };
            }

            if (_customerExchangeRepository.GetAllByCustomerIDInLastHour
                (request.CustomerID).Where(x => x.Status.Count == 2).Count() >= 10)
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("Limitation"
                 + Guid.NewGuid().ToString(), "Your exchange limitation overflowed"));
                return new Result() { FailedResults = request.ValidationResult };
            }

            if (transaction.CustomerID != request.CustomerID)
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("AccessDenied"
              + Guid.NewGuid().ToString(), "Access denied"));
                return new Result() { FailedResults = request.ValidationResult };
            }

            transaction.ExchangeCompleted();
            transaction.UpdateTransactionDate();

            _customerExchangeRepository.Update(transaction);
            await _customerExchangeRepository.Save();

            return new Result();
        }
        public bool IsValid()
        {
            ValidationResult = new FinalizeTransactionCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
