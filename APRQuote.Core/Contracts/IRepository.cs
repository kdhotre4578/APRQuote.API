using System;

namespace APRQuote.Core.Contracts
{
    public interface IRepository<T> : IBaseRepository<T> where T : class
    {
        void Update(T t);

        void Delete(T t);
    }
}
