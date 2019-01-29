using FluentAssertions;
using Waes.Diff.Core.Extensions;
using Xunit;

namespace Waes.Diff.Core.UnitTests.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("abc123", false)]
        [InlineData("YWJjMTIz", true)]
        [InlineData("dGVzdGUgZGUgZW5jb2RlZA==", true)]
        [InlineData("test not base64", false)]
        public void IsBase64String_ShouldVerifyIfGivenValueIsBase64(string value, bool isBase64)
        {
            value.IsBase64String().Should().Be(isBase64);
        }
    }
}
