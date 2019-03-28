using System.Linq;
using System.Threading.Tasks;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Infra.Interfaces;

namespace Waes.Assignment.Infra.Repositories
{
    public class DiffRepository : Repository<Diff>, IDiffRepository
    {
        public DiffRepository(IDatabase<Diff> database) : base(database)
        {
        }

        public async Task<Diff> GetByCorrelationId(string correlationId)
        {
            return Database.Entities.FirstOrDefault(x => x.CorrelationId == correlationId);
        }
    }
}
