﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Waes.Assignment.Application.Exceptions;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Domain.Commands;
using Waes.Assignment.Domain.Events;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Application.CommandHandlers
{
    public class PayLoadCommandHandler : IRequestHandler<PayLoadCreateCommand, bool>
    {
        private readonly IMediatorHandler _bus;

        private readonly IPayLoadRepository _payLoadRepository;

        public PayLoadCommandHandler(IMediatorHandler bus, IPayLoadRepository payLoadRepository)
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
            _payLoadRepository = payLoadRepository ?? throw new ArgumentNullException(nameof(payLoadRepository));
        }

        public async Task<bool> Handle(PayLoadCreateCommand request, CancellationToken cancellationToken)
        {
            var payLoadFromRepository = await _payLoadRepository.GetByCorrelationIdAndSide(request.CorrelationId, request.Side);

            if (payLoadFromRepository != null)
            {
                throw new EntityAlreadyExistsException($"Payload of correlation id {request.CorrelationId} is already taken.");
            }

            var payLoad = new PayLoad(request.CorrelationId, request.Content, request.Side);

            await _payLoadRepository.Add(payLoad);

            await _bus.RaiseEvent(new PayLoadCreatedEvent(payLoad.Id, payLoad.CorrelationId, payLoad.Content, payLoad.Side));

            return true;
        }
    }
}
