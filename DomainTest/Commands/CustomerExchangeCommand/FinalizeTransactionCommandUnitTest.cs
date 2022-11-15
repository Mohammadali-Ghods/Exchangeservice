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
using Moq;
using Microsoft.Extensions.Logging;

namespace DomainTest.Commands.CustomerExchangeCommand
{
    public class FinalizeTransactionCommandUnitTest
    {
        private CustomerExchangeFakeRepository customerExchangeFakeRepository;
        private FakeCacheManagement fakeCacheManagement;
        private CallUpCommand precommand;
        private string Id;
        private Mock<ILogger<FinalizeTransactionCommand>> logger =
            new Mock<ILogger<FinalizeTransactionCommand>>();
        public FinalizeTransactionCommandUnitTest()
        {
            Id = Guid.NewGuid().ToString();
            customerExchangeFakeRepository =
                new CustomerExchangeFakeRepository();
            fakeCacheManagement = new FakeCacheManagement();
        }
        [Fact]
        public async Task Handler_FinalizeTransactionCommandWithCurrectCommand_UpdateDBAndFinalize()
        {
            precommand = new CallUpCommand()
            {
                ID = Id,
                Amount = 100,
                CustomerID = Guid.NewGuid().ToString(),
                ConvertedValue = 1856,
                FromExchange = 152,
                ToExchange = 186,
                ExchangeRate = 18.56f,
            };

            CallUpCommand callUpCommand = new CallUpCommand(customerExchangeFakeRepository,
                fakeCacheManagement);
            await callUpCommand.Handle(precommand, new System.Threading.CancellationToken());

            FinalizeTransactionCommand command = new FinalizeTransactionCommand()
            {
                ID = precommand.ID,
                CustomerID = precommand.CustomerID,
            };

            FinalizeTransactionCommand finalizeTransactionCommand =
                new FinalizeTransactionCommand(customerExchangeFakeRepository,
                fakeCacheManagement, logger.Object);
            await finalizeTransactionCommand.Handle(command,
                new System.Threading.CancellationToken());

            var result = await customerExchangeFakeRepository.GetById(command.ID);
            Assert.True(result.Status.Count == 2);
        }

        [Fact]
        public async Task Handler_FinalizeTransactionCommandDifferentCustomer_ReturnError()
        {
            precommand = new CallUpCommand()
            {
                ID = Guid.NewGuid().ToString(),
                Amount = 100,
                CustomerID = Guid.NewGuid().ToString(),
                ConvertedValue = 1856,
                FromExchange = 152,
                ToExchange = 186,
                ExchangeRate = 18.56f,
            };

            CallUpCommand callUpCommand = new CallUpCommand(customerExchangeFakeRepository,
                fakeCacheManagement);
            await callUpCommand.Handle(precommand, new System.Threading.CancellationToken());

            FinalizeTransactionCommand command = new FinalizeTransactionCommand()
            {
                ID = precommand.ID,
                CustomerID = Guid.NewGuid().ToString(),
            };

            FinalizeTransactionCommand finalizeTransactionCommand =
                new FinalizeTransactionCommand(customerExchangeFakeRepository
                , fakeCacheManagement,logger.Object);
            var result = await finalizeTransactionCommand.Handle(command,
                 new System.Threading.CancellationToken());

            Assert.NotNull(result.FailedResults);
        }
    }
}
