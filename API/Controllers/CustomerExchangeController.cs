using Application.Interfaces;
using Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class CustomerExchangeController : ApiController
    {
        private readonly ICustomerExchangeAppService _customerExchangeAppService;
        private ILogger<CustomerExchangeViewModel> _logger;
        public CustomerExchangeController(ICustomerExchangeAppService customerExchangeAppService,

            ILogger<CustomerExchangeViewModel> logger)
        {
            _customerExchangeAppService = customerExchangeAppService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("customerexchange/admin")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<CustomerExchangeViewModel> Get()
        {
            return _customerExchangeAppService.GetAll();
        }

        [HttpGet("customerexchange/admin/{transactionid}")]
        [Authorize(Roles = "Admin")]
        public async Task<CustomerExchangeViewModel> Get(string transactionid)
        {
            return await _customerExchangeAppService.GetById(transactionid);
        }

        [HttpGet("customerexchange/customer/{transactionid}")]
        [Authorize(Roles = "Customer")]
        public async Task<CustomerExchangeViewModel> GetCustomer(string transactionid)
        {
            return await _customerExchangeAppService.GetByTransaction(transactionid,User.Identity.Name);
        }

        [HttpGet("customerexchange/customer")]
        [Authorize(Roles = "Customer")]
        public IEnumerable<CustomerExchangeViewModel> GetCustomer()
        {
            return _customerExchangeAppService.GetByCustomer(User.Identity.Name);
        }

        [HttpPost]
        [Route("customerexchange/customer/callup")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CallUp([FromBody] CallUpViewModel viewmodel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse
                (await _customerExchangeAppService.CallUp(viewmodel,User.Identity.Name));
        }

        [HttpPost]
        [Route("customerexchange/customer/finalizetransaction")]
        [AllowAnonymous]
        public async Task<IActionResult> FinalizeTransaction([FromBody] FinalizeTransactionViewModel viewmodel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) :
                CustomResponse(await _customerExchangeAppService.FinalizeTransaction(viewmodel,User.Identity.Name));
        }
    }
}
