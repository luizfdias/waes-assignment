using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Domain.Commands;

namespace Waes.Assignment.Application.Validations
{
    public class PayLoadCreateCommandValidation : AbstractValidator<PayLoadCreateCommand>
    {
        private readonly IPayLoadRepository _payLoadRepository;

        public PayLoadCreateCommandValidation(IPayLoadRepository payLoadRepository)
        {
            _payLoadRepository = payLoadRepository ?? throw new ArgumentNullException(nameof(payLoadRepository));

            WhenAsync(NotExistAsync, () =>
            {

            });
        }

        private async Task<bool> NotExistAsync(PayLoadCreateCommand correlationId, CancellationToken cancellationToken)
        {
            var payload = await _payLoadRepository.GetByCorrelationId(correlationId);

            return payload == null;
        }
    }
}
