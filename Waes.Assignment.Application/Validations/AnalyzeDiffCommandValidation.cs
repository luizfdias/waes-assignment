using FluentValidation;
using Waes.Assignment.Domain.Commands;

namespace Waes.Assignment.Application.Validations
{
    public class AnalyzeDiffCommandValidation : AbstractValidator<AnalyzeDiffCommand>
    {
        public AnalyzeDiffCommandValidation()
        {
            RuleFor(x => x.CorrelationId).NotEmpty();
        }
    }
}
