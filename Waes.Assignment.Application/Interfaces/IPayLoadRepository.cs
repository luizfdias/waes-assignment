using System.Collections.Generic;
using System.Threading.Tasks;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Domain.Models.Enums;

namespace Waes.Assignment.Application.Interfaces
{
    /// <summary>
    /// The repository interface of <see cref="PayLoad"/> 
    /// </summary>
    public interface IPayLoadRepository : IRepository<PayLoad>
    {
        /// <summary>
        /// Gets the <see cref="PayLoad"/> by correlationId
        /// </summary>
        /// <param name="correlationid"></param>
        /// <returns></returns>
        Task<IEnumerable<PayLoad>> GetByCorrelationId(string correlationid);

        /// <summary>
        /// Gets the <see cref="PayLoad"/> by correlationId and <see cref="SideEnum"/>
        /// </summary>
        /// <param name="correlationid"></param>
        /// <param name="side"></param>
        /// <returns></returns>
        Task<PayLoad> GetByCorrelationIdAndSide(string correlationid, SideEnum side);
    }
}
