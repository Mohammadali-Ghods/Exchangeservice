using Bus.ConfigurationModel;
using Domain.Interfaces;
using MassTransit;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Bus
{
    public class RabbitMQMessageHandler : IMessageBus
    {
        private readonly IBus _bus;
        private readonly RabbitMQModel _rabbitmqdata;
    }
}
