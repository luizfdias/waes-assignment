using System.Threading.Tasks;

namespace Waes.Diff.Api.Interfaces
{
    public interface IHandleRequest<TRequest, TResponse>
    {
        Task<TResponse> Handle(TRequest request);
    }
}
