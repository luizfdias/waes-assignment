using FluentAssertions;
using System.Text;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Domain.Services;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Domain.Models
{
    public class DiffEngineTests
    {
        [Theory, AutoNSubstituteData]
        public void ProcessDiff_WhenBytesContentAreEqual_MustReturnEqual(DiffEngine sut, string correlationId)
        {
            var left = Encoding.ASCII.GetBytes("abc123");
            var right = Encoding.ASCII.GetBytes("abc123");

            var result = sut.ProcessDiff(correlationId, left, right);

            result.Should().BeOfType<EqualDiff>();
        }

        [Theory, AutoNSubstituteData]
        public void ProcessDiff_WhenBytesContentAreNotEqual_MustReturnNotEqual(DiffEngine sut, string correlationId)
        {
            var left = Encoding.ASCII.GetBytes("abc123");
            var right = Encoding.ASCII.GetBytes("aYZ123");

            var result = sut.ProcessDiff(correlationId, left, right);

            result.Should().BeOfType<NotEqualDiff>();
            //TODO:
        }

        [Theory, AutoNSubstituteData]
        public void ProcessDiff_WhenBytesContentAreNotOfEqualSize_MustReturnNotOfEqualSize(DiffEngine sut, string correlationId)
        {
            var left = Encoding.ASCII.GetBytes("abc123");
            var right = Encoding.ASCII.GetBytes("abc12345");

            var result = sut.ProcessDiff(correlationId, left, right);

            result.Should().BeOfType<NotOfEqualSizeDiff>();
        }
    }
}
