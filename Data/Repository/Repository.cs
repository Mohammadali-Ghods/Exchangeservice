using Data.EFBase;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ExchangeDBContext _context = null;
        protected DbSet<T> table = null;
        public Repository()
        {
            this._context = new ExchangeDBContext();
            table = _context.Set<T>();
        }
        public Repository(ExchangeDBContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table;
        }
        public async Task<T> GetById(object id)
        {
            return await table.FindAsync(id);
        }
        public async Task Insert(T obj)
        {
           await table.AddAsync(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public async Task Delete(object id)
        {
            T existing =await table.FindAsync(id);
            table.Remove(existing);
        }
        public async Task Save()
        {
           await _context.SaveChangesAsync();
        }


    }
}
