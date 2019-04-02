using FluentAssertions;
using Waes.Assignment.Application.Validations;
using Waes.Assignment.Domain.Commands;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Application.Validations
{
    public class AnalyzeDiffCommandValidationTests
    {
        [Theory, AutoNSubstituteData]
        public void Validate_WhenCorrelationIdIsFilled_IsValidShouldBeTrue(AnalyzeDiffCommandValidation sut, AnalyzeDiffCommand command)
        {
            sut.Validate(command).IsValid.Should().BeTrue();
        }

        [Theory, AutoNSubstituteData]        
        public void Validate_WhenCorrelationIdIsNull_IsValidShouldBeFalse(AnalyzeDiffCommandValidation sut)
        {
            var command = new AnalyzeDiffCommand(null);

            sut.Validate(command).IsValid.Should().BeFalse();
        }

        [Theory, AutoNSubstituteData]
        public void Validate_WhenCorrelationIdIsEmpty_IsValidShouldBeFalse(AnalyzeDiffCommandValidation sut)
        {
            var command = new AnalyzeDiffCommand(string.Empty);

            sut.Validate(command).IsValid.Should().BeFalse();
        }
    }
}
