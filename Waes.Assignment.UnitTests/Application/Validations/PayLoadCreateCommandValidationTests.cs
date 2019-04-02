using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Application.Validations;
using Waes.Assignment.Domain.Commands;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Domain.Models.Enums;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Application.Validations
{
    public class PayLoadCreateCommandValidationTests
    {
        private readonly IPayLoadRepository _payLoadRepository;

        private readonly PayLoadCreateCommandValidation _sut;

        public PayLoadCreateCommandValidationTests()
        {
            _payLoadRepository = Substitute.For<IPayLoadRepository>();
                        
            _sut = new PayLoadCreateCommandValidation(_payLoadRepository);
        }

        [Theory, AutoNSubstituteData]
        public void Constructor_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(PayLoadCreateCommandValidation).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public void Validate_WhenCommandIsValidAndPayLoadNotExists_IsValidShouldBeTrue(byte[] content)
        {
            var command = new PayLoadCreateCommand("123456", content, SideEnum.Left);

            _payLoadRepository.GetByCorrelationIdAndSide(Arg.Any<string>(), Arg.Any<SideEnum>()).ReturnsNull();

            var validationResult = _sut.Validate(command);

            validationResult.IsValid.Should().BeTrue();
        }

        [Theory, AutoNSubstituteData]
        public void Validate_WhenPayLoadAlreadyExists_IsValidShouldBeFalse(byte[] content, PayLoad payLoad)
        {
            var command = new PayLoadCreateCommand("123456", content, SideEnum.Left);

            _payLoadRepository.GetByCorrelationIdAndSide(command.CorrelationId, command.Side).Returns(payLoad);

            var validationResult = _sut.Validate(command);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().OnlyContain(x => x.ErrorCode.Equals("PayloadAlreadyExists"));
        }

        [Theory, AutoNSubstituteData]
        public void Validate_WhenCorrelationIdIsNull_IsValidShouldBeFalse(byte[] content)
        {
            var command = new PayLoadCreateCommand(null, content, SideEnum.Left);

            _payLoadRepository.GetByCorrelationIdAndSide(Arg.Any<string>(), Arg.Any<SideEnum>()).ReturnsNull();

            var validationResult = _sut.Validate(command);

            validationResult.IsValid.Should().BeFalse();
        }

        [Theory, AutoNSubstituteData]
        public void Validate_WhenCorrelationIdIsEmpty_IsValidShouldBeFalse(byte[] content)
        {
            var command = new PayLoadCreateCommand(string.Empty, content, SideEnum.Left);

            _payLoadRepository.GetByCorrelationIdAndSide(Arg.Any<string>(), Arg.Any<SideEnum>()).ReturnsNull();

            var validationResult = _sut.Validate(command);

            validationResult.IsValid.Should().BeFalse();
        }

        [Theory, AutoNSubstituteData]
        public void Validate_WhenSideIsInvalid_IsValidShouldBeFalse(byte[] content)
        {
            var command = new PayLoadCreateCommand("123456", content, SideEnum.Undefined);

            _payLoadRepository.GetByCorrelationIdAndSide(Arg.Any<string>(), Arg.Any<SideEnum>()).ReturnsNull();

            var validationResult = _sut.Validate(command);

            validationResult.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_WhenContentIsNull_IsValidShouldBeFalse()
        {
            var command = new PayLoadCreateCommand("123456", null, SideEnum.Left);

            _payLoadRepository.GetByCorrelationIdAndSide(Arg.Any<string>(), Arg.Any<SideEnum>()).ReturnsNull();

            var validationResult = _sut.Validate(command);

            validationResult.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_WhenContentIsEmpty_IsValidShouldBeFalse()
        {
            var command = new PayLoadCreateCommand("123456", new byte[0], SideEnum.Left);

            _payLoadRepository.GetByCorrelationIdAndSide(Arg.Any<string>(), Arg.Any<SideEnum>()).ReturnsNull();

            var validationResult = _sut.Validate(command);

            validationResult.IsValid.Should().BeFalse();
        }
    }
}
