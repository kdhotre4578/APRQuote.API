using System;
using System.Threading.Tasks;
using APRQuote.Core.Models;

namespace APRQuote.Core.Contracts
{
    public interface IUoW
    {
        Task<int> Commit();

        void Dispose();
    }
}
