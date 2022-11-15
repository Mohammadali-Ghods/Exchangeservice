using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Domain.Commands.CustomerExchangeCommands;
using DomainTest.FakeRepository;
using DomainTest.FakeCache;

namespace DomainTest.Commands.CustomerExchangeCommand
{
    public class CallUpCommandUnitTest
    {
        private CustomerExchangeFakeRepository customerExchangeFakeRepository;
        private FakeCacheManagement fakeCacheManagement;
        private CallUpCommand precommand;
        public CallUpCommandUnitTest()
        {
            customerExchangeFakeRepository =
                new CustomerExchangeFakeRepository();
            fakeCacheManagement = new FakeCacheManagement();
        }
        [Fact]
        public async Task Handler_CallUpCommandWorkflow_InsertIntoDBSucceed()
        {
            CallUpCommand command = new CallUpCommand() 
            {
                ID=Guid.NewGuid().ToString(),
                Amount=100,
                CustomerID=Guid.NewGuid().ToString(),
                ConvertedValue=1856,
                FromExchange=152,
                ToExchange=186,
                ExchangeRate=18.56f,
            };
            
            CallUpCommand callUpCommand = new CallUpCommand(customerExchangeFakeRepository
                ,fakeCacheManagement);
            await callUpCommand.Handle(command,new System.Threading.CancellationToken());

            Assert.NotNull(customerExchangeFakeRepository.GetById(command.ID));
        }

        [Fact]
        public async Task Handler_CallUpCommandWorkflowWithWrongCustomerID_ReturnError()
        {
            CallUpCommand command = new CallUpCommand()
            {
                ID = Guid.NewGuid().ToString(),
                Amount = 100,
                CustomerID = "125",
                ConvertedValue = 1856,
                FromExchange = 152,
                ToExchange = 186,
                ExchangeRate = 18.56f,
            };

            CallUpCommand callUpCommand = new CallUpCommand(customerExchangeFakeRepository,
                fakeCacheManagement);
            var result= await callUpCommand.Handle(command, new System.Threading.CancellationToken());

            Assert.NotNull(result.FailedResults);
            Assert.Null(await customerExchangeFakeRepository.GetById(command.ID));
        }
    }
}
