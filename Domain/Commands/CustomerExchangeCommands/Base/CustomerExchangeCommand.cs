using MediatR;
using System;
using FluentValidation.Results;
using System.Collections.Generic;
using Domain.ResultModel;
using MongoDB.Entities;
using Domain.Interfaces.Base;

namespace Domain.Commands.CustomerExchangeCommands.Base
{
    public class CustomerExchangeCommand : IRequest<Result>
    {
        public string ID { get; set; }
        public string CustomerID { get; set; }
        public int FromExchange { get; set; }
        public int ToExchange { get; set; }
        public float Amount { get; set; }
        public float ExchangeRate { get; set; }
        public float ConvertedValue { get; set; }
        public ValidationResult ValidationResult { get; set; }
    }
   
}