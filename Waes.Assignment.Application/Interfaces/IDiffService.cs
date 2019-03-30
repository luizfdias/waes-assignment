using System.Threading.Tasks;
using Waes.Assignment.Application.ApiModels;

namespace Waes.Assignment.Application.Interfaces
{
    public interface IDiffService
    {
        Task<DiffResponse> Get(string correlationId);
    }
}
