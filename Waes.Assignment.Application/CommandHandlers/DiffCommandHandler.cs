using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Domain.Commands;
using Waes.Assignment.Domain.Events;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models.Enums;

namespace Waes.Assignment.Application.CommandHandlers
{
    /// <summary>
    /// DiffCommandHandler handles the <see cref="AnalyzeDiffCommand"/>
    /// </summary>
    public class DiffCommandHandler : IRequestHandler<AnalyzeDiffCommand, bool>
    {
        private readonly IMediatorHandler _bus;

        private readonly IDiffEngine _diffEngine;

        private readonly IPayLoadRepository _payLoadRepository;

        private readonly ICache _cache;

        /// <summary>
        /// Initializes a new instance of <see cref="DiffCommandHandler"/>
        /// </summary>
        /// <param name="bus"></param>
        /// <param name="diffEngine"></param>
        /// <param name="payLoadRepository"></param>
        /// <param name="cache"></param>
        public DiffCommandHandler(IMediatorHandler bus, IDiffEngine diffEngine, IPayLoadRepository payLoadRepository,
            ICache cache)
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
            _diffEngine = diffEngine ?? throw new ArgumentNullException(nameof(diffEngine));
            _payLoadRepository = payLoadRepository ?? throw new ArgumentNullException(nameof(payLoadRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        /// <summary>
        /// It analyzes the diff and raises a <see cref="DiffAnalyzedEvent"/> if it goes right.                
        /// The diff will be analyze only if both payloads (left and right) exists.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(AnalyzeDiffCommand request, CancellationToken cancellationToken)
        {
            var payLoads = await _payLoadRepository.GetByCorrelationId(request.CorrelationId);

            var left = payLoads.FirstOrDefault(x => x.Side == SideEnum.Left);
            var right = payLoads.FirstOrDefault(x => x.Side == SideEnum.Right);

            if (left == null || right == null)
                return false;

            var diff = _diffEngine.ProcessDiff(request.CorrelationId, left.Content, right.Content);

            /* Reading the assignment, looks like to me the diff result is a read-only data and I believe is 
            something that doesn't need to be stored forever. With that in mind I assumed a cache solution would 
            be a good approach to maintain the data avaiable.
            I am using a memory cache with 1 day expiration, just for demonstrations purposes, but I created an abstraction 
            that could fit for distributed cache solutions, like redis. */
            await _cache.SetAsync($"diff_{request.CorrelationId}", diff, 86400);

            await _bus.RaiseEvent(new DiffAnalyzedEvent(diff.Id, diff.CorrelationId));

            return true;
        }
    }
}
