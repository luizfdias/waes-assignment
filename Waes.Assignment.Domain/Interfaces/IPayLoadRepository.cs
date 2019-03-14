using System.Collections.Generic;
using System.Threading.Tasks;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Domain.Interfaces
{
    public interface IPayLoadRepository : IRepository<PayLoad>
    {
        Task<IEnumerable<PayLoad>> GetByCorrelationId(string correlationid);
    }
}
