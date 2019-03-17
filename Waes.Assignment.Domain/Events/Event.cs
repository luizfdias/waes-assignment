using MediatR;
using System;

namespace Waes.Assignment.Domain.Events
{
    public class Event : INotification, IRequest<bool>
    {
        public Guid AggregateId { get; protected set; }
    }
}
