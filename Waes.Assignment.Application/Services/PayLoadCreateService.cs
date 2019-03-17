using AutoMapper;
using System;
using System.Threading.Tasks;
using Waes.Assignment.Api.ViewModels;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Domain.Events;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Application.Services
{
    public class PayLoadCreateService : IPayLoadCreateService
    {
        private readonly IPayLoadRepository _payLoadRepository;

        private readonly IMapper _mapper;

        private readonly IEventRaiser _eventRaiser;

        public PayLoadCreateService(IPayLoadRepository payLoadRepository, IMapper mapper,
            IEventRaiser eventRaiser)
        {
            _payLoadRepository = payLoadRepository ?? throw new ArgumentNullException(nameof(payLoadRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _eventRaiser = eventRaiser ?? throw new ArgumentNullException(nameof(eventRaiser));
        }

        public async Task<CreatePayLoadResponse> CreateNewPayload(string correlationId, CreatePayLoadRequest request)
        {            
            var payLoad = _mapper.Map<PayLoad>(request, opt => opt.Items["correlationId"] = correlationId);

            var payLoadFromRepository = await _payLoadRepository.GetByCorrelationIdAndSide(correlationId, payLoad.Side);

            if (payLoadFromRepository != null)
            {
                await _eventRaiser.RaiseEvent(
                    new PayLoadAlreadyCreatedEvent(payLoadFromRepository.Id, payLoadFromRepository.CorrelationId, payLoadFromRepository.Side));

                return null;
            }

            await _payLoadRepository.Add(payLoad);

            await _eventRaiser.RaiseEvent(
                new PayLoadCreatedEvent(payLoad.Id, payLoad.CorrelationId, payLoad.Content, payLoad.Side));

            return _mapper.Map<CreatePayLoadResponse>(payLoad);
        }
    }
}
