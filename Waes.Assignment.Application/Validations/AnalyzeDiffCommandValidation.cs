using FluentValidation;
using Waes.Assignment.Domain.Commands;

namespace Waes.Assignment.Application.Validations
{
    /// <summary>
    /// Validator for <see cref="AnalyzeDiffCommand"/>
    /// </summary>
    public class AnalyzeDiffCommandValidation : AbstractValidator<AnalyzeDiffCommand>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AnalyzeDiffCommandValidation"/>
        /// </summary>
        public AnalyzeDiffCommandValidation()
        {
            RuleFor(x => x.CorrelationId).NotEmpty();
        }
    }
}
