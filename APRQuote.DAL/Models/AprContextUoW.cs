using APRQuote.Contracts;
using APRQuote.DAL.Context;
using System;

namespace APRQuote.DAL.Models
{
    public class AprContextUoW : IUoW
    {
        public AprQuoteDbContext dbContext { get; }

        public AprContextUoW(string connectionString)
        {
            dbContext = new AprQuoteDbContext(connectionString);
        }

        public AprContextUoW(AprQuoteDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Commit()
        {
            dbContext.SaveChanges();
        }

        public void Rollback()
        {
            dbContext.Dispose();
        }
    }
}
