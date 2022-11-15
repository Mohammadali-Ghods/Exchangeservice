using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalApi.Models
{
    public class Exchange
    {
       public float result { get; set; }
        public Info info { get; set; }
    }
    public class Info 
    {
        public float rate { get; set; }
    }
}
