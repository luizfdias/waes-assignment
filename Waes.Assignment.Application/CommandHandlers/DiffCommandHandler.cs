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
    public class DiffCommandHandler : IRequestHandler<AnalyzeDiffCommand, bool>
    {
        private readonly IMediatorHandler _bus;

        private readonly IDiffEngine _diffEngine;

        private readonly IPayLoadRepository _payLoadRepository;

        private readonly IDiffRepository _diffRepository;

        public DiffCommandHandler(IMediatorHandler bus, IDiffEngine diffEngine, IPayLoadRepository payLoadRepository,
            IDiffRepository diffRepository)
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
            _diffEngine = diffEngine ?? throw new ArgumentNullException(nameof(diffEngine));
            _payLoadRepository = payLoadRepository ?? throw new ArgumentNullException(nameof(payLoadRepository));
            _diffRepository = diffRepository ?? throw new ArgumentNullException(nameof(diffRepository));
        }

        public async Task<bool> Handle(AnalyzeDiffCommand request, CancellationToken cancellationToken)
        {
            var payLoads = await _payLoadRepository.GetByCorrelationId(request.CorrelationId);

            var left = payLoads.FirstOrDefault(x => x.Side == SideEnum.Left);
            var right = payLoads.FirstOrDefault(x => x.Side == SideEnum.Right);

            if (left == null || right == null)
                return false;

            var diff = _diffEngine.ProcessDiff(request.CorrelationId, left.Content, right.Content);

            await _diffRepository.Add(diff);

            await _bus.RaiseEvent(new DiffAnalyzedEvent(diff.Id, diff.CorrelationId));

            return true;
        }
    }
}
