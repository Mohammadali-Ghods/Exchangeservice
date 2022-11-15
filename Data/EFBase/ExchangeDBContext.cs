using Domain.Models;
using Domain.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EFBase
{
    public class ExchangeDBContext : DbContext
    {
        public ExchangeDBContext(DbContextOptions<ExchangeDBContext> options
            ) :base(options)
        {
        }
        public ExchangeDBContext()
        {
        }

        public DbSet<CustomerExchange> CustomerExchange { get; set; }
        public DbSet<Symbol> Symbol { get; set; }
        public DbSet<Status> Status { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
               "Data Source=docker.codeandsolution.com;Initial Catalog=CustomerExchange;User ID=exchange;Password=Aa@43320098");
            }
        }
    }
    public class ExchangeDBContextFactory : IDesignTimeDbContextFactory<ExchangeDBContext>
    {
        public ExchangeDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ExchangeDBContext>();
            optionsBuilder.UseSqlServer("Data Source=docker.codeandsolution.com;Initial Catalog=CustomerExchange;User ID=exchange;Password=Aa@43320098");

            return new ExchangeDBContext(optionsBuilder.Options);
        }
    }
   
}
