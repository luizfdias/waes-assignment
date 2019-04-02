using FluentValidation;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Domain.Commands;
using Waes.Assignment.Domain.Models.Enums;

namespace Waes.Assignment.Application.Validations
{
    /// <summary>
    /// Validator for <see cref="PayLoadCreateCommand"/>
    /// </summary>
    public class PayLoadCreateCommandValidation : AbstractValidator<PayLoadCreateCommand>
    {
        private readonly IPayLoadRepository _payLoadRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="PayLoadCreateCommandValidation"/>
        /// </summary>
        /// <param name="payLoadRepository"></param>
        public PayLoadCreateCommandValidation(IPayLoadRepository payLoadRepository)
        {
            _payLoadRepository = payLoadRepository ?? throw new ArgumentNullException(nameof(payLoadRepository));

            RuleFor(x => x.CorrelationId).NotEmpty();
            RuleFor(x => x.Side).NotEqual(SideEnum.Undefined);
            RuleFor(x => x.Content).NotEmpty();

            When(x => !string.IsNullOrWhiteSpace(x.CorrelationId), () =>
            {
                RuleFor(x => x).MustAsync(NotExistAsync)
                .WithMessage(x => $"Payload of correlation id {x.CorrelationId} is already taken.")
                .WithErrorCode("PayloadAlreadyExists");
            });
        }

        private async Task<bool> NotExistAsync(PayLoadCreateCommand payLoadCreateCommand, CancellationToken cancellationToken)
        {
            var payload = await _payLoadRepository.GetByCorrelationIdAndSide(
                payLoadCreateCommand.CorrelationId, 
                payLoadCreateCommand.Side);

            return payload == null;
        }
    }
}
