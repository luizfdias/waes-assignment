using System.Threading.Tasks;
using Waes.Assignment.Application.ViewModels;

namespace Waes.Assignment.Application.Interfaces
{
    public interface IDiffService
    {
        Task<DiffResponse> Get(string correlationId);
    }
}
