using System.Threading.Tasks;

namespace Waes.Diff.Api.Interfaces
{
    public interface IMediator
    {
        Task<TResponse> Send<TRequest, TResponse>(TRequest request);
    }
}
