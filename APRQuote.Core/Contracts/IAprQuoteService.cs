using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APRQuote.Core.Models;

namespace APRQuote.Core.Contracts
{
    public interface IAprQuoteService
    {
        Task<IEnumerable<AprQuote>> GetAllQuotes();

        Task<AprQuote> GetQuote(int id);

        Task<bool> AddQuote(AprQuote aprQuote);
    }
}
