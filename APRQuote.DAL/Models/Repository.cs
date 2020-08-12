using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using APRQuote.Core.Contracts;
using APRQuote.DAL.Context;
using APRQuote.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace APRQuote.DAL.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private AprQuoteDbContext _dbContext;

        public Repository(AprQuoteDbContext aprQuoteDbContext)
        {
            _dbContext = aprQuoteDbContext;
            EnsureDbCreated();
        }

        public Repository(string connectionString)
        {
            _dbContext = new AprQuoteDbContext(connectionString);
            EnsureDbCreated();
        }

        public Repository(IUoW uoW)
        {
            _dbContext = ((AprContextUoW)uoW).dbContext;
            EnsureDbCreated();
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await _dbContext.Set<T>().AsQueryable<T>().ToListAsync();
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression)
        {
            if (expression == null)
            {
                return null;
            }

            return await _dbContext.Set<T>().AsQueryable<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<bool> Add(T t)
        {
            if (t != null)
            {
                await _dbContext.Set<T>().AddAsync(t);
                return true;
            }

            return false;
        }

        public void Update(T t)
        {
            if (t != null)
            {
                _dbContext.Set<T>().Update(t);
            }
        }

        public void Delete(T t)
        {
            if (t != null)
            {
                _dbContext.Set<T>().Remove(t);
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void SetUoW(IUoW uoW)
        {
            this._dbContext = ((AprContextUoW)uoW).dbContext;
        }

        private void EnsureDbCreated()
        {
            if (_dbContext.Database.EnsureCreated())
            {
                new TestData().SeedDataAprQuote(_dbContext);
            }
        }
    }

}
