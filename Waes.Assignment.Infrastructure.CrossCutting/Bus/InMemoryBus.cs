﻿using MediatR;
using System.Threading.Tasks;
using Waes.Assignment.Domain.Events;
using Waes.Assignment.Domain.Interfaces;

namespace Waes.Assignment.Infrastructure.CrossCutting.Bus
{
    public sealed class InMemoryBus : IEventRaiser
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            return _mediator.Publish(@event);
        }
    }
}