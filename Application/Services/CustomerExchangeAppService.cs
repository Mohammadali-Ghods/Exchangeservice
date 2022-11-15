using Application.Interfaces;
using Application.ViewModel;
using AutoMapper;
using Domain.Commands.CustomerExchangeCommands;
using Domain.Interfaces;
using Domain.Interfaces.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CustomerExchangeAppService : ICustomerExchangeAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private ICustomerExchangeRepository _customerExchangeRepository;
        private IExchangeApi _exchangeApi;
        private ISymbolRepository _symbolRepository;

        public CustomerExchangeAppService(IMapper mapper,
                                  IMediator mediator,
                                   ICustomerExchangeRepository customerExchangeRepository,
                                   IExchangeApi exchangeApi,
                                   ISymbolRepository symbolRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _customerExchangeRepository = customerExchangeRepository;
            _exchangeApi = exchangeApi;
            _symbolRepository = symbolRepository;
        }

        public async Task<ResultModel> CallUp(CallUpViewModel commandmodel, string customerid)
        {
            var fromexchange = await _symbolRepository.GetById(commandmodel.FromExchange);
            var toexchange = await _symbolRepository.GetById(commandmodel.ToExchange);

            var thirdpartyexchangeresult =
                await _exchangeApi.Convert(fromexchange.Name, toexchange.Name, commandmodel.Amount);

            var command = _mapper.Map<CallUpCommand>(commandmodel);
            command.ExchangeRate = thirdpartyexchangeresult.Rate;
            command.CustomerID = customerid;
            command.ID = Guid.NewGuid().ToString();
            command.ConvertedValue = thirdpartyexchangeresult.FinalValue;

            var result = await _mediator.Send(command);

            return new ResultModel()
            {
                FailedResults = result.FailedResults,
                SucceedResult = new CustomerExchangeViewModel() { ID = command.ID,
                Amount=commandmodel.Amount,ExchangeRate=thirdpartyexchangeresult.Rate,
                ConvertedValue=thirdpartyexchangeresult.FinalValue,CustomerID=customerid,
                FromExchange=commandmodel.FromExchange,ToExchange=commandmodel.ToExchange}
            };
        }

        public async Task<ResultModel> FinalizeTransaction(FinalizeTransactionViewModel commandmodel, string customerid)
        {
            var command = _mapper.Map<FinalizeTransactionCommand>(commandmodel);
            command.CustomerID = customerid;

            var result = await _mediator.Send(command);

            return new ResultModel() { FailedResults = result.FailedResults };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public IEnumerable<CustomerExchangeViewModel> GetAll()
        {
            return _mapper.Map<List<CustomerExchangeViewModel>>
                (_customerExchangeRepository.GetAll()) ;
        }

        public IEnumerable<CustomerExchangeViewModel> GetByCustomer(string customerid)
        {
            return _mapper.Map<List<CustomerExchangeViewModel>>
                (_customerExchangeRepository.GetAllByCustomerID(customerid));
        }

        public async Task<CustomerExchangeViewModel> GetById(string transactionid)
        {
            return _mapper.Map<CustomerExchangeViewModel>
               (await _customerExchangeRepository.GetById(transactionid));
        }

        public async Task<CustomerExchangeViewModel> GetByTransaction(string transactionid, string customerid)
        {
            return _mapper.Map<CustomerExchangeViewModel>
              (await _customerExchangeRepository.GetById(transactionid));
        }
    }
}
