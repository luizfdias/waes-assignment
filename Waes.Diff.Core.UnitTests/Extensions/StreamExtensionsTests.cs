using FluentAssertions;
using System.IO;
using Waes.Diff.Core.Extensions;
using Waes.Diff.Core.UnitTests.AutoData;
using Xunit;

namespace Waes.Diff.Core.UnitTests.Extensions
{
    public class StreamExtensionsTests
    {
        [Theory, AutoNSubstituteData]
        public async void ConvertToByteArrayAsync_WhenStreamIsNotEmpty_ShouldConvertItToByteArray(byte[] buffer)
        {            
            var result = await new MemoryStream(buffer).ConvertToByteArrayAsync();

            result.Should().BeEquivalentTo(buffer);
        }        
    }
}
