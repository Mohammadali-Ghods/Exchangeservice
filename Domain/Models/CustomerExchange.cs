using Domain.ValueObject;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class CustomerExchange
    {
        public CustomerExchange(string id, string customerid,
            int fromexchange, int toexchange, float amount, float exchangerate,float convertedvalue)
        {
            ID = id;
            CustomerID = customerid;
            FromExchange = fromexchange;
            ToExchange = toexchange;
            Amount = amount;
            ExchangeRate = exchangerate;
            Status = new List<Status>();
            Status.Add(new Status(StatusTypes.CallUpFinished));
            ConvertedValue = convertedvalue;
        }

        // Empty constructor for EF
        protected CustomerExchange() { }
        public string ID { get; private set; }
        public string CustomerID { get; private set; }
        public int FromExchange { get; private set; }
        public int ToExchange { get; private set; }
        public float Amount { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public float ExchangeRate { get; private set; }
        public float ConvertedValue { get; private set; }
        public List<Status> Status { get; private set; }
        public void ExchangeCompleted() => Status.Add(new Status(StatusTypes.ExchangeCompleted));
        public void UpdateExchangeRate(float newvalue) => ExchangeRate = newvalue;
        public void UpdateAmount(float newvalue) => Amount = newvalue;
        public void UpdateTransactionDate() => TransactionDate = DateTime.Now;

    }
}