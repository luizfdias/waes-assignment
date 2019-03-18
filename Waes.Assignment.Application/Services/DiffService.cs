using AutoMapper;
using System;
using System.Threading.Tasks;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Application.ViewModels;
using Waes.Assignment.Domain.Events;
using Waes.Assignment.Domain.Interfaces;

namespace Waes.Assignment.Application.Services
{
    public class DiffService : IDiffService
    {
        private readonly IDiffRepository _diffRepository;

        private IMediatorHandler _eventRaiser;

        private readonly IMapper _mapper;

        public DiffService(IDiffRepository diffRepository, IMapper mapper, IMediatorHandler eventRaiser)
        {
            _diffRepository = diffRepository ?? throw new ArgumentNullException(nameof(diffRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _eventRaiser = eventRaiser ?? throw new ArgumentNullException(nameof(eventRaiser));
        }

        public async Task<DiffResponse> Get(string correlationId)
        {
            var diff = await _diffRepository.GetByCorrelationId(correlationId);

            if (diff == null)
                await _eventRaiser.RaiseEvent(new DiffNotFoundEvent(correlationId));

            return _mapper.Map<DiffResponse>(diff);
        }
    }
}
