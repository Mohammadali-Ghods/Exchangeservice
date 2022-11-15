using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class SymbolRepository : Repository<Symbol>, ISymbolRepository
    {
        public IEnumerable<Symbol> GetByName(string name)
        {
            return table.Where(x => x.Name == name).ToList();
        }
    }
}
