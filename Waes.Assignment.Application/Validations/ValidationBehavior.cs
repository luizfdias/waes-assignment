using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Waes.Assignment.Domain.Commands;

namespace Waes.Assignment.Application.Validations
{
    /// <summary>
    /// ValidationBehavior contains the logic to validate any command that is sent through the application
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : Command
    {
        private readonly IValidator<TRequest> _validator;

        /// <summary>
        /// Initializes a new instance of <see cref="ValidationBehavior{TRequest, TResponse}"/>
        /// </summary>
        /// <param name="validator"></param>
        public ValidationBehavior(IValidator<TRequest> validator)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        /// <summary>
        /// It calls the validator and check if command is valid
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        /// <exception cref="ValidationException">Thrown when payload already exists</exception>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validationResult = _validator.Validate<TRequest>(request);

            if (!validationResult.IsValid)
            {                
                throw new ValidationException(validationResult.Errors);
            }

            return await next();
        }
    }
}
