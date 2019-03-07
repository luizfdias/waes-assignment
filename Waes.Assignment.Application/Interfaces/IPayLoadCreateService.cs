using System.Threading.Tasks;
using Waes.Assignment.Api.ViewModels;

namespace Waes.Assignment.Application.Interfaces
{
    public interface IPayLoadCreateService
    {
        Task<CreatePayLoadResponse> CreateNewPayload(string correlationId, CreatePayLoadRequest request);
    }
}
