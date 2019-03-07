using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Infrastructure.Databases.InMemory.Repositories
{
    public class PayLoadRepository : IRepository<PayLoad>
    {
        public InMemoryDatabase Database { get; }

        public PayLoadRepository(InMemoryDatabase database)
        {
            Database = database;
        }        

        public Task<PayLoad> Add(PayLoad item)
        {
            Database.PayLoads.Add(item);

            return Task.FromResult(item);
        }

        public Task<IEnumerable<PayLoad>> Get(Expression<Func<PayLoad, bool>> predicate)
        {
            return Task.FromResult<IEnumerable<PayLoad>>(Database.PayLoads.AsQueryable().Where(predicate));
        }
    }
}
