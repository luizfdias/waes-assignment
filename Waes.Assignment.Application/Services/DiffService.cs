using AutoMapper;
using System;
using System.Threading.Tasks;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Application.ViewModels;
using Waes.Assignment.Domain.Interfaces;

namespace Waes.Assignment.Application.Services
{
    public class DiffService : IDiffService
    {
        private readonly IDiffRepository _diffRepository;

        private readonly IMapper _mapper;

        public DiffService(IDiffRepository diffRepository, IMapper mapper, IMediatorHandler eventRaiser)
        {
            _diffRepository = diffRepository ?? throw new ArgumentNullException(nameof(diffRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<DiffResponse> Get(string correlationId)
        {
            var diff = await _diffRepository.GetByCorrelationId(correlationId);

            return _mapper.Map<DiffResponse>(diff);
        }
    }
}
