using System;
using System.Threading.Tasks;
using Waes.Diff.Api.Interfaces;

namespace Waes.Diff.Api
{
    public class Mediator : IMediator
    {
        public IServiceProvider ServiceFactory { get; }

        public Mediator(IServiceProvider serviceFactory)
        {
            ServiceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
        }

        public async Task<TResponse> Send<TRequest, TResponse>(TRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var handler = (IHandleRequest<TRequest, TResponse>)ServiceFactory.GetService(typeof(IHandleRequest<TRequest, TResponse>));

            return await handler.Handle(request);
        }
    }
}
