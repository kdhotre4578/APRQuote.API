using System;
using APRQuote.Core.Models;

namespace APRQuote.Core.Contracts
{
    public interface IAprUoW : IUoW
    {
        IRepository<Vehicle> VehicleRepository { get; }

        IRepository<QuoteType> QuoteTypeRepository { get; }

        IRepository<APRPercentRange> APRPercentRangeRepository { get; }

        IRepository<Quote> QuoteRepository { get; }
    }
}
