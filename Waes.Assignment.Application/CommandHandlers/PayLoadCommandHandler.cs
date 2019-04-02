using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Waes.Assignment.Application.Exceptions;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Domain.Commands;
using Waes.Assignment.Domain.Events;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Application.CommandHandlers
{
    /// <summary>
    /// PayLoadCommandHandler handles the <see cref="PayLoadCreateCommand"/>
    /// </summary>
    public class PayLoadCommandHandler : IRequestHandler<PayLoadCreateCommand, bool>
    {
        private readonly IMediatorHandler _bus;

        private readonly IPayLoadRepository _payLoadRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="PayLoadCommandHandler"/>
        /// </summary>
        /// <param name="bus"></param>
        /// <param name="payLoadRepository"></param>
        public PayLoadCommandHandler(IMediatorHandler bus, IPayLoadRepository payLoadRepository)
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
            _payLoadRepository = payLoadRepository ?? throw new ArgumentNullException(nameof(payLoadRepository));
        }

        /// <summary>
        /// It handles the creation of new payload and raises a <see cref="PayLoadCreatedEvent"/> if it goes right        
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>        
        public async Task<bool> Handle(PayLoadCreateCommand request, CancellationToken cancellationToken)
        {            
            var payLoad = new PayLoad(request.CorrelationId, request.Content, request.Side);

            await _payLoadRepository.Add(payLoad);

            await _bus.RaiseEvent(new PayLoadCreatedEvent(payLoad.Id, payLoad.CorrelationId, payLoad.Content, payLoad.Side));

            return true;
        }
    }
}
