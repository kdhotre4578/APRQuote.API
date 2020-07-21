using System;

namespace APRQuote.Contracts
{
    public interface IRepository<T> : IBaseRepository<T> where T : class
    {
        void Update(T t);

        void Delete(T t);
    }
}
