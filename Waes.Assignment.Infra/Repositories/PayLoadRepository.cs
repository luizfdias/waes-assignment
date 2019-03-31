using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Domain.Models.Enums;
using Waes.Assignment.Infra.Interfaces;

namespace Waes.Assignment.Infra.Repositories
{
    /// <summary>
    /// The <see cref="PayLoad"/> repository
    /// </summary>
    public class PayLoadRepository : Repository<PayLoad>, IPayLoadRepository
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PayLoadRepository"/>
        /// </summary>
        /// <param name="database"></param>
        public PayLoadRepository(IDatabase<PayLoad> database) : base(database)
        {
        }

        /// <summary>
        /// Gets the <see cref="IEnumerable{PayLoad}"/> by correlation id
        /// </summary>
        /// <param name="correlationid"></param>
        /// <returns></returns>
        public async Task<IEnumerable<PayLoad>> GetByCorrelationId(string correlationid)
        {
            return Database.Entities.Where(x => x.CorrelationId == correlationid);
        }

        /// <summary>
        /// Gets the <see cref="PayLoad"/> by correlation id and <see cref="SideEnum"/>
        /// </summary>
        /// <param name="correlationid"></param>
        /// <param name="side"></param>
        /// <returns></returns>
        public async Task<PayLoad> GetByCorrelationIdAndSide(string correlationid, SideEnum side)
        {
            return Database.Entities.FirstOrDefault(x => x.CorrelationId == correlationid && x.Side == side);
        }
    }
}
