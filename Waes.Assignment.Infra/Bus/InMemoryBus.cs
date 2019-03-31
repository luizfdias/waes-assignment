using MediatR;
using System;
using System.Threading.Tasks;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Domain.Commands;
using Waes.Assignment.Domain.Events;

namespace Waes.Assignment.Infra.Bus
{
    /// <summary>
    /// In represents an in memory bus to raise events and send commands through the application
    /// </summary>
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of <see cref="InMemoryBus"/> with an instance of <see cref="IMediator"/> 
        /// </summary>
        /// <param name="mediator"></param>
        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Raises an event of <see cref="Event"/> type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        /// <returns></returns>
        public Task RaiseEvent<T>(T @event) where T : Event
        {
            return _mediator.Publish(@event);
        }

        /// <summary>
        /// Sends a command of <see cref="Command"/> type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }
    }
}
