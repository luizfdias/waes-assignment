using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Infrastructure.Repositories.InMemory
{
    public class PayLoadRepository : Repository<PayLoad>, IPayLoadRepository
    {
        public PayLoadRepository(InMemoryDatabase<PayLoad> database) : base(database)
        {
        }

        public async Task<IEnumerable<PayLoad>> GetByCorrelationId(string correlationid)
        {
            return Database.Entities.Where(x => x.CorrelationId == correlationid);
        }
    }
}
