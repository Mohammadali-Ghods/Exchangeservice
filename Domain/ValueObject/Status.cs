using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObject
{
    public class Status
    {
        public Status(StatusTypes type) 
        {
            StatusDate=DateTime.Now;
            Type=type;
        }
        public int ID { get; private set; }
        public DateTime StatusDate { get;private set; }
        public StatusTypes Type { get;private set; }
    }
    public enum StatusTypes
    {
        CallUpFinished,
        ExchangeCompleted,
    }
}
