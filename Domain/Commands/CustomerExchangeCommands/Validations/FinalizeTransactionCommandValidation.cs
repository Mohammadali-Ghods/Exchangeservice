using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.CustomerExchangeCommands.Validations
{
    public class FinalizeTransactionCommandValidation:CustomerExchangeValidation<FinalizeTransactionCommand>
    {
        public FinalizeTransactionCommandValidation() 
        {
            ValidateID();
        }
    }
}
