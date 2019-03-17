using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Waes.Assignment.Domain.Commands;
using Waes.Assignment.Domain.Events;
using Waes.Assignment.Domain.Interfaces;

namespace Waes.Assignment.Application.NotificationHandlers
{
    public class DiffListener : 
        INotificationHandler<PayLoadCreatedEvent>
    {
        private readonly IMediatorHandler _bus;

        private readonly IMapper _mapper;

        public DiffListener(IMediatorHandler bus, IMapper mapper, IPayLoadRepository payLoadRepository)
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }        

        public Task Handle(PayLoadCreatedEvent notification, CancellationToken cancellationToken)
        {                        
            var command = _mapper.Map<AnalyzeDiffCommand>(notification.CorrelationId);
            return _bus.SendCommand(command);
        }
    }
}
