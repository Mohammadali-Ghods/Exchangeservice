using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Data.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Bus;
using Domain.ResultModel;
using ExternalApi.Api;
using Domain.Commands.CustomerExchangeCommands;
using Domain.Interfaces.Base;
using Domain.Models;
using Data.Catching;

namespace IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application
            services.AddScoped<ICustomerExchangeAppService, CustomerExchangeAppService>();
            services.AddScoped<ISymbolAppService, SymbolAppService>();
            // Domain - Events

            // Domain - Commands
            services.AddScoped<IRequestHandler<CallUpCommand, Result>, CallUpCommand>();
            services.AddScoped<IRequestHandler<FinalizeTransactionCommand, Result>, FinalizeTransactionCommand>();
           

            // Infra - Data
            services.AddScoped<ICustomerExchangeRepository, CustomerExchangeRepository>();
            services.AddScoped<ISymbolRepository, SymbolRepository>();
            services.AddScoped<ICacheManagement<CustomerExchange>, CacheManagement<CustomerExchange>>();
            services.AddScoped<ICacheManagement<Symbol>, CacheManagement<Symbol>>();

            // Infra - Bus
            services.AddScoped<IMessageBus,RabbitMQMessageHandler>();

            // Infra - ExternalApi
            services.AddScoped<IExchangeApi, ExchangeApi>();
        }
    }
}