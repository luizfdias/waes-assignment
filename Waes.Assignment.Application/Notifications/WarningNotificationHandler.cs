using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Waes.Assignment.Domain.Events;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models.Enums;

namespace Waes.Assignment.Application.Notifications
{
    //TODO Extrair toda lógica para uma outra camada e criar eventos de domínio
    public class WarningNotificationHandler : INotificationHandler<PayLoadCreatedEvent>
    {
        private readonly IDiffDomainService _diffDomainService;

        private readonly IPayLoadRepository _payLoadRepository;

        public WarningNotificationHandler(IDiffDomainService diffDomainService, IPayLoadRepository payLoadRepository)
        {
            _diffDomainService = diffDomainService;
            _payLoadRepository = payLoadRepository;
        }        

        public async Task Handle(PayLoadCreatedEvent notification, CancellationToken cancellationToken)
        {
            var oppositePayLoad = await _payLoadRepository.GetByCorrelationIdAndSide(
                notification.CorrelationId, 
                notification.Side == SideEnum.Left ? SideEnum.Right : SideEnum.Left);

            if (oppositePayLoad == null)
                return;

            var diffResult = _diffDomainService.ProcessDiff(oppositePayLoad.Content, notification.Content);
        }
    }
}
