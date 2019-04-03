using AutoMapper;
using System;
using System.Threading.Tasks;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Application.ApiModels;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Application.Services
{
    /// <summary>
    /// DiffService gets the difference already analyzed between payloads
    /// </summary>
    public class DiffService : IDiffService
    {
        private readonly ICache _cache;

        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of <see cref="DiffService"/>
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="mapper"></param>
        public DiffService(ICache cache, IMapper mapper)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets the diff as a <see cref="DiffResponse"/>
        /// </summary>
        /// <param name="correlationId"></param>
        /// <returns></returns>
        public async Task<DiffResponse> Get(string correlationId)
        {            
            var diff = await _cache.GetAsync<Diff>($"diff_{correlationId}");

            return _mapper.Map<DiffResponse>(diff);
        }
    }
}
