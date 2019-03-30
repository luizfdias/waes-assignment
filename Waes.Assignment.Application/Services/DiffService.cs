using AutoMapper;
using System;
using System.Threading.Tasks;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Application.ApiModels;

namespace Waes.Assignment.Application.Services
{
    public class DiffService : IDiffService
    {
        private readonly IDiffRepository _diffRepository;

        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of <see cref="DiffService"/>
        /// </summary>
        /// <param name="diffRepository"></param>
        /// <param name="mapper"></param>
        public DiffService(IDiffRepository diffRepository, IMapper mapper)
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
