using MediatR;
using System;

namespace Waes.Assignment.Domain.Events
{
    /// <summary>
    /// Event abstraction
    /// </summary>
    public class Event : INotification, IRequest<bool>
    {
        /// <summary>
        /// The id of the entity
        /// </summary>
        public Guid EntityId { get; protected set; }
    }
}
