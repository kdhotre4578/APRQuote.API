using System;
using System.Collections.Generic;
using APRQuote.Core.Models;

namespace APRQuote.Core.Contracts
{
    public interface IAprQuote
    {
        IEnumerable<AprQuote> GetAllQuotes();

        AprQuote GetQuote(int id);

        bool AddQuote(AprQuote aprQuote);
    }
}
