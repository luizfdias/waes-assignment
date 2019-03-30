using System.Threading.Tasks;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Application.Interfaces
{
    public interface IDiffRepository : IRepository<Diff>
    {
        Task<Diff> GetByCorrelationId(string correlationid);
    }
}
