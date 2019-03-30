using AutoMapper;
using System;
using System.Threading.Tasks;
using Waes.Assignment.Application.ApiModels;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Domain.Commands;
using Waes.Assignment.Domain.Events;

namespace Waes.Assignment.Application.Services
{
    public class PayLoadService : IPayLoadService
    {
        private readonly IMediatorHandler _bus;

        private readonly IMapper _mapper;

        private readonly INotificationHandler _notificationHandler;

        /// <summary>
        /// Initializes a new instance of <see cref="PayLoadService"/>
        /// </summary>
        /// <param name="bus"></param>
        /// <param name="mapper"></param>
        /// <param name="notificationHandler"></param>
        public PayLoadService(IMediatorHandler bus, IMapper mapper, INotificationHandler notificationHandler)
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _notificationHandler = notificationHandler ?? throw new ArgumentNullException(nameof(notificationHandler));
        }

        public async Task<CreatePayLoadResponse> Create(string correlationId, CreatePayLoadRequest request)
        {            
            var command = _mapper.Map<PayLoadCreateCommand>(request, opt => opt.Items["correlationId"] = correlationId);
            await _bus.SendCommand(command);

            var createdPayLoadEvent = _notificationHandler.GetEvent<PayLoadCreatedEvent>();

            return _mapper.Map<CreatePayLoadResponse>(createdPayLoadEvent);
        }
    }
}


