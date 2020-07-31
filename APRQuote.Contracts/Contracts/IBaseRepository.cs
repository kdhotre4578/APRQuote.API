using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace APRQuote.Core.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> Get();

        T Get(Expression<Func<T, bool>> expression);

        bool Add(T t);

        void SetUoW(IUoW uoW);

        void Save();
    }
}
