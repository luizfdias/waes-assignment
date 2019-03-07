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
        private readonly IRepository<PayLoad> _payLoadRepository;

        private readonly IMapper _mapper;

        private readonly IMediator _mediator;

        public PayLoadCreateService(IRepository<PayLoad> payLoadRepository, IMapper mapper,
            IMediator mediator)
        {
            _payLoadRepository = payLoadRepository ?? throw new System.ArgumentNullException(nameof(payLoadRepository));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
        }        

        public async Task<CreatePayLoadResponse> CreateNewPayload(string correlationId, CreatePayLoadRequest request)
        {            
            var payLoad = _mapper.Map<PayLoad>(request, opt => opt.Items["correlationId"] = correlationId);

            var payLoadFromRepository = await _payLoadRepository.Get(x => x.CorrelationId == correlationId && x.Side == payLoad.Side);

            if (payLoadFromRepository.Any())
            {
                await _mediator.Publish(
                    new WarningNotification(request.GetType().Name,
                    $"There is already a PayLoad with correlation Id | {correlationId} | and side | {payLoad.Side.ToString()} |",
                    NotificationType.ResourceDuplicated)).ConfigureAwait(false);

                return await Task.FromResult(default(CreatePayLoadResponse));
            }

            var result = await _payLoadRepository.Add(payLoad).ConfigureAwait(false);

            return _mapper.Map<CreatePayLoadResponse>(result);
        }
    }
}
