using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace APRQuote.Core.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> Get();

        Task<T> Get(Expression<Func<T, bool>> expression);

        Task<bool> Add(T t);

        void SetUoW(IUoW uoW);

        void Save();
    }
}
