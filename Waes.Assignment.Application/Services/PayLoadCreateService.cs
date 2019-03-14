using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using Waes.Assignment.Api.ViewModels;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Application.Notifications;
using Waes.Assignment.Application.Notifications.Enums;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Application.Services
{
    public class PayLoadCreateService : IPayLoadCreateService
    {
        private readonly IPayLoadRepository _payLoadRepository;

        private readonly IMapper _mapper;

        private readonly IMediator _mediator;

        public PayLoadCreateService(IPayLoadRepository payLoadRepository, IMapper mapper,
            IMediator mediator)
        {
            _payLoadRepository = payLoadRepository ?? throw new System.ArgumentNullException(nameof(payLoadRepository));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
        }        

        public async Task<CreatePayLoadResponse> CreateNewPayload(string correlationId, CreatePayLoadRequest request)
        {            
            var payLoad = _mapper.Map<PayLoad>(request, opt => opt.Items["correlationId"] = correlationId);

            var payLoadFromRepository = await _payLoadRepository.GetByCorrelationId(correlationId);

            if (payLoadFromRepository.Any(x => x.Side == payLoad.Side))
            {
                await _mediator.Publish(
                    new WarningNotification(request.GetType().Name,
                    $"There is already a PayLoad with correlation Id | {correlationId} | and side | {payLoad.Side.ToString()} |",
                    NotificationType.ResourceDuplicated));

                return await Task.FromResult(default(CreatePayLoadResponse));
            }

            await _payLoadRepository.Add(payLoad);

            return _mapper.Map<CreatePayLoadResponse>(payLoad);
        }
    }
}
