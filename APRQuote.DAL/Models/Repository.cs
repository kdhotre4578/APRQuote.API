using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using APRQuote.Contracts;
using APRQuote.DAL.Context;
using APRQuote.DAL.Data;

namespace APRQuote.DAL.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private AprQuoteDbContext _dbContext;

        public Repository(AprQuoteDbContext aprQuoteDbContext)
        {
            _dbContext = aprQuoteDbContext;
        }

        public Repository(string connectionString)
        {
            _dbContext = new AprQuoteDbContext(connectionString);

            if (_dbContext.Database.EnsureCreated())
            {
                new TestData().SeedDataAprQuote(_dbContext);
            }
        }

        public Repository(IUoW uoW)
        {
            _dbContext = ((AprContextUoW)uoW).dbContext;
        }

        public IEnumerable<T> Get()
        {
            return _dbContext.Set<T>().AsQueryable<T>().ToList();
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            if (expression == null)
            {
                return null;
            }

            return _dbContext.Set<T>().AsQueryable<T>().FirstOrDefault(expression);
        }

        public bool Add(T t)
        {
            if (t != null)
            {
                _dbContext.Set<T>().Add(t);
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
    }

}
