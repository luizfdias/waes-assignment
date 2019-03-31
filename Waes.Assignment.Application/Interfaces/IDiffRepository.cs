using System.Threading.Tasks;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Application.Interfaces
{
    /// <summary>
    /// The repository interface of <see cref="Diff"/>
    /// </summary>
    public interface IDiffRepository : IRepository<Diff>
    {
        /// <summary>
        /// Gets the <see cref="Diff"/> by the correlation id
        /// </summary>
        /// <param name="correlationid"></param>
        /// <returns></returns>
        Task<Diff> GetByCorrelationId(string correlationid);
    }
}
