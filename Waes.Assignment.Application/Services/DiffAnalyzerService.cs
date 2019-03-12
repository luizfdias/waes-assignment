using AutoMapper;
using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Application.Notifications;
using Waes.Assignment.Application.Notifications.Enums;
using Waes.Assignment.Application.ViewModels;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Domain.Models.Enums;

namespace Waes.Assignment.Application.Services
{
    public class DiffAnalyzerService : IDiffAnalyzerService
    {
        private readonly IRepository<PayLoad> _payLoadRepository;

        private readonly IDiffDomainService<byte> _diffService;

        private IMediator _mediator;

        private readonly IMapper _mapper;

        public DiffAnalyzerService(IRepository<PayLoad> payLoadRepository, IDiffDomainService<byte> diffEngine, IMapper mapper,
            IMediator mediator)
        {
            _payLoadRepository = payLoadRepository ?? throw new ArgumentNullException(nameof(payLoadRepository));
            _diffService = diffEngine ?? throw new ArgumentNullException(nameof(diffEngine));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }        

        public async Task<DiffResponse> Analyze(string correlationId)
        {
            var payLoads = await _payLoadRepository.Get(x => x.CorrelationId == correlationId);

            if (!payLoads.Any())
            {
                await _mediator.Publish(
                    new WarningNotification(correlationId.GetType().Name,
                    $"The PayLoad with correlation id | {correlationId} | was not found.",
                    NotificationType.NotFound)).ConfigureAwait(false);

                return await Task.FromResult(default(DiffResponse));
            }

            var result = _diffService.ProcessDiff(
                payLoads.First(x => x.Side == SideEnum.Left).Content, 
                payLoads.First(x => x.Side == SideEnum.Right).Content);

            return _mapper.Map<DiffResponse>(result);
        }
    }
}
