using System.Collections.Generic;
using System.Threading.Tasks;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Domain.Models.Enums;

namespace Waes.Assignment.Application.Interfaces
{
    public interface IPayLoadRepository : IRepository<PayLoad>
    {
        Task<IEnumerable<PayLoad>> GetByCorrelationId(string correlationid);

        Task<PayLoad> GetByCorrelationIdAndSide(string correlationid, SideEnum side);
    }
}
