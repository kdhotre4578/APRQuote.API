using System;
using APRQuote.Core.Models;

namespace APRQuote.Core.Contracts
{
    public interface IUoW
    {
        void Commit();

        void Dispose();
    }
}
