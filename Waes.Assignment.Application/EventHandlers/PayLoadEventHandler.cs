using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Domain.Commands;
using Waes.Assignment.Domain.Events;

namespace Waes.Assignment.Application.EventHandlers
{
    /// <summary>
    /// PayLoadEventHandler handles a <see cref="PayLoadCreatedEvent"/> 
    /// </summary>
    public class PayLoadEventHandler : INotificationHandler<PayLoadCreatedEvent>
    {
        private readonly IMediatorHandler _bus;

        /// <summary>
        /// Initializes a new instance of <see cref="PayLoadEventHandler"/>
        /// </summary>
        /// <param name="bus"></param>
        public PayLoadEventHandler(IMediatorHandler bus)
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        /// <summary>
        /// It handles a <see cref="PayLoadCreatedEvent"/> creating a <see cref="AnalyzeDiffCommand"/> from that sending it to the bus
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(PayLoadCreatedEvent notification, CancellationToken cancellationToken)
        {
            var command = new AnalyzeDiffCommand(notification.CorrelationId);
            return _bus.SendCommand(command);
        }
    }
}
