using System;

namespace APRQuote.Contracts
{
    public interface IUoW
    {
        void Commit();

        void Rollback();
    }
}
