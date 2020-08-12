using APRQuote.Core.Models;
using APRQuote.Core.Contracts;
using APRQuote.DAL.Context;
using System;
using System.Threading.Tasks;

namespace APRQuote.DAL.Models
{
    public class AprContextUoW : IAprUoW
    {
        #region private variables

        IRepository<Vehicle> _vehicleRepository;

        IRepository<QuoteType> _quoteTypeRepository;

        IRepository<APRPercentRange> _aprPercentRangeRepository;

        IRepository<Quote> _quoteRepository;

        #endregion

        #region Properties
        
        public AprQuoteDbContext dbContext { get; }

        public IRepository<Vehicle> VehicleRepository => _vehicleRepository ??= new Repository<Vehicle>(dbContext);

        public IRepository<QuoteType> QuoteTypeRepository => _quoteTypeRepository ??= new Repository<QuoteType>(dbContext);

        public IRepository<APRPercentRange> APRPercentRangeRepository => _aprPercentRangeRepository ??= new Repository<APRPercentRange>(dbContext);

        public IRepository<Quote> QuoteRepository => _quoteRepository ??= new Repository<Quote>(dbContext);
        
        #endregion

        #region constructor
        
        public AprContextUoW(string connectionString)
        {
            dbContext = new AprQuoteDbContext(connectionString);
        }

        public AprContextUoW(AprQuoteDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        #endregion

        #region public methods

        public async Task<int> Commit()
        {
            return await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
        
        #endregion
    }
}
