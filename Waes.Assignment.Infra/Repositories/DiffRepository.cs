using System.Linq;
using System.Threading.Tasks;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Infra.Interfaces;

namespace Waes.Assignment.Infra.Repositories
{
    /// <summary>
    /// The <see cref="Diff"/> repository
    /// </summary>
    public class DiffRepository : Repository<Diff>, IDiffRepository
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DiffRepository"/>
        /// </summary>
        /// <param name="database"></param>
        public DiffRepository(IDatabase<Diff> database) : base(database)
        {
        }

        /// <summary>
        /// Gets the <see cref="Diff"/> by correlation id
        /// </summary>
        /// <param name="correlationId"></param>
        /// <returns></returns>
        public async Task<Diff> GetByCorrelationId(string correlationId)
        {
            return Database.Entities.FirstOrDefault(x => x.CorrelationId == correlationId);
        }
    }
}
