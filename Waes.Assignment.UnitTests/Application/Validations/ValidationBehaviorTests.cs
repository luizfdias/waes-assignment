using AutoFixture.Idioms;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using NSubstitute;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Waes.Assignment.Application.Validations;
using Waes.Assignment.Domain.Commands;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Application.Validations
{
    public class ValidationBehaviorTests
    {
        private readonly IValidator<PayLoadCreateCommand> _validator;

        private readonly ValidationBehavior<PayLoadCreateCommand, bool> _sut;

        public ValidationBehaviorTests()
        {
            _validator = Substitute.For<IValidator<PayLoadCreateCommand>>();

            _sut = new ValidationBehavior<PayLoadCreateCommand, bool>(_validator);
        }

        [Theory, AutoNSubstituteData]
        public void Constructor_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(ValidationBehavior<,>).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async void Handle_WhenValidationResultIsValid_ShouldCallNextAndReturnTrue(PayLoadCreateCommand command)
        {
            var validationResult = new ValidationResult();

            _validator.Validate<PayLoadCreateCommand>(Arg.Any<PayLoadCreateCommand>()).ReturnsForAnyArgs(validationResult);

            var next = new RequestHandlerDelegate<bool>(() => Task.FromResult(true));

            var result = await _sut.Handle(command, new CancellationToken(), next);

            result.Should().BeTrue();
        }

        [Theory, AutoNSubstituteData]
        public async void Handle_WhenValidationResultIsInvalid_ShouldThrowValidationException(PayLoadCreateCommand command, List<ValidationFailure> validationFailures)
        {
            var validationResult = new ValidationResult(validationFailures);

            _validator.Validate<PayLoadCreateCommand>(Arg.Any<PayLoadCreateCommand>()).ReturnsForAnyArgs(validationResult);

            var next = new RequestHandlerDelegate<bool>(() => Task.FromResult(true));

            var ex = await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, new CancellationToken(), next));
            ex.Errors.Should().BeEquivalentTo(validationFailures);
        }
    }
}
