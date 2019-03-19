using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Waes.Assignment.Domain.Commands;
using Waes.Assignment.Domain.Events;
using Waes.Assignment.Domain.Interfaces;

namespace Waes.Assignment.Domain.EventHandlers
{
    public class PayLoadEventHandler : INotificationHandler<PayLoadCreatedEvent>
    {
        private readonly IMediatorHandler _bus;

        public PayLoadEventHandler(IMediatorHandler bus)
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }        

        public Task Handle(PayLoadCreatedEvent notification, CancellationToken cancellationToken)
        {
            var command = new AnalyzeDiffCommand(notification.CorrelationId);
            return _bus.SendCommand(command);
        }
    }
}
