using FluentAssertions;
using System.Text;
using Waes.Assignment.Domain.Models.Enums;
using Waes.Assignment.Domain.Services;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Domain.Services
{
    public class DiffDomainServiceTests
    {
        [Theory, AutoNSubstituteData]
        public void ProcessDiff_WhenBytesContentAreEqual_MustReturnEqual(DiffDomainService sut)
        {
            var left = Encoding.ASCII.GetBytes("abc123");
            var right = Encoding.ASCII.GetBytes("abc123");

            var result = sut.ProcessDiff(left, right);

            result.Status.Should().Be(DiffStatus.Equal);
        }

        [Theory, AutoNSubstituteData]
        public void ProcessDiff_WhenBytesContentAreNotEqual_MustReturnNotEqual(DiffDomainService sut)
        {
            var left = Encoding.ASCII.GetBytes("abc123");
            var right = Encoding.ASCII.GetBytes("aYZ123");

            var result = sut.ProcessDiff(left, right);

            result.Status.Should().Be(DiffStatus.NotEqual);
        }

        [Theory, AutoNSubstituteData]
        public void ProcessDiff_WhenBytesContentAreNotOfEqualSize_MustReturnNotOfEqualSize(DiffDomainService sut)
        {
            var left = Encoding.ASCII.GetBytes("abc123");
            var right = Encoding.ASCII.GetBytes("abc12345");

            var result = sut.ProcessDiff(left, right);

            result.Status.Should().Be(DiffStatus.NotOfEqualSize);
        }
    }
}
