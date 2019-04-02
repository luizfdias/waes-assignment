using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Waes.Assignment.Application.Exceptions;

namespace Waes.Assignment.Application.Validations
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest> _validator;

        public ValidationBehavior(IValidator<TRequest> validator)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        /// <exception cref="ValidationException">Thrown when payload already exists</exception>
        /// <exception cref="EntityAlreadyExistsException">Thrown when payload already exists</exception>
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validationResult = _validator.Validate<TRequest>(request);

            if (!validationResult.IsValid)
            {
                var payLoadAlreadyExistsError = validationResult.Errors.FirstOrDefault(x => x.ErrorCode.Contains("PayloadAlreadyExists", StringComparison.InvariantCultureIgnoreCase));

                if (payLoadAlreadyExistsError != null)
                    throw new EntityAlreadyExistsException(payLoadAlreadyExistsError.ErrorMessage);

                throw new ValidationException(validationResult.Errors);
            }

            return next();
        }        
    }
}
