using System;
using System.Linq;
using System.Threading.Tasks;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Infrastructure.Repositories.InMemory
{
    public class DiffRepository : Repository<Diff>, IDiffRepository
    {
        public DiffRepository(InMemoryDatabase<Diff> database) : base(database)
        {
        }

        public Task<Diff> GetByCorrelationId(string correlationid)
        {
            return Database.Entities.FirstOrDefault(x => x.CorrelationId == correlationid);
        }
    }
}
