using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Waes.Assignment.Domain.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> Add(T item);

        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate);
    }
}
