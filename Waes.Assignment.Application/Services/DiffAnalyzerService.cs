using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Application.ViewModels;
using Waes.Assignment.Domain.Events;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models.Enums;

namespace Waes.Assignment.Application.Services
{
    public class DiffAnalyzerService : IDiffAnalyzerService
    {
        private readonly IPayLoadRepository _payLoadRepository;

        private readonly IDiffDomainService _diffService;

        private IEventRaiser _eventRaiser;

        private readonly IMapper _mapper;

        public DiffAnalyzerService(IPayLoadRepository payLoadRepository, IDiffDomainService diffEngine, IMapper mapper,
            IEventRaiser eventRaiser)
        {
            _payLoadRepository = payLoadRepository ?? throw new ArgumentNullException(nameof(payLoadRepository));
            _diffService = diffEngine ?? throw new ArgumentNullException(nameof(diffEngine));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _eventRaiser = eventRaiser ?? throw new ArgumentNullException(nameof(eventRaiser));
        }        

        public async Task<DiffResponse> Analyze(string correlationId)
        {
            var payLoads = await _payLoadRepository.GetByCorrelationId(correlationId);

            //TODO: Verificar se tem lado direito e esquerdo
            if (!payLoads.Any())
            {
                await _eventRaiser.RaiseEvent(
                    new PayLoadNotFoundEvent());

                return null;
            }

            var result = _diffService.ProcessDiff(
                payLoads.First(x => x.Side == SideEnum.Left).Content, 
                payLoads.First(x => x.Side == SideEnum.Right).Content);

            return _mapper.Map<DiffResponse>(result);
        }
    }
}
