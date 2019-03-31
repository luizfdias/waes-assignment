using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using Waes.Assignment.Application.Attributes;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Application.Attributes
{
    public class ContentRequiredAttributeTests
    {
        private readonly ContentRequiredAttribute _sut;

        public ContentRequiredAttributeTests()
        {
            _sut = new ContentRequiredAttribute();
        }

        [Fact]
        public void IsValid_WhenValueIsNull_ShouldReturnsFalse()
        {
            var result = _sut.IsValid((byte[])null);

            result.Should().BeFalse();
        }

        [Fact]
        public void IsValid_WhenValueIsEmpty_ShouldReturnsFalse()
        {
            var result = _sut.IsValid(new byte[0]);
            
            result.Should().BeFalse();            
        }

        [Theory, AutoNSubstituteData]
        public void IsValid_WhenValueIsEmpty_ShouldReturnsErrorMessageAsExpected(ValidationContext validationContext)
        {
            _sut.GetValidationResult(new byte[0], validationContext).ErrorMessage.Should().Be("Content must not be null or empty.");            
        }

        [Theory, AutoNSubstituteData]
        public void IsValid_WhenValueIsNull_ShouldReturnsErrorMessageAsExpected(ValidationContext validationContext)
        {
            _sut.GetValidationResult((byte[])null, validationContext).ErrorMessage.Should().Be("Content must not be null or empty.");
        }

        [Fact]
        public void IsValid_WhenValueHasSomething_ShouldReturnsTrue()
        {
            var result = _sut.IsValid(new byte[] { 1, 2 });

            result.Should().BeTrue();
        }
    }
}
