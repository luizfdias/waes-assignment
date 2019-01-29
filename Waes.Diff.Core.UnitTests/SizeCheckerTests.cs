using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using Waes.Diff.Core.Models;
using Waes.Diff.Core.UnitTests.AutoData;
using Waes.Diff.Core.UnitTests.Helpers;
using Xunit;

namespace Waes.Diff.Core.UnitTests
{
    public class SizeCheckerTests
    {
        [Theory, AutoNSubstituteData]
        public void Constructors_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(SizeChecker).GetConstructors());
        }

        [Theory]
        [InlineNSubstituteData("abc123", "abc123", true)]
        [InlineNSubstituteData("abc123", "XYZ987", true)]
        [InlineNSubstituteData("   ", "   ", true)]
        [InlineNSubstituteData("abc1234", "abc123", false)]
        [InlineNSubstituteData("    ", "  ", false)]
        public void Check_WhenBytesAreProvided_ShouldReturnSameSizeAsExpected(string leftData, string rightData, bool resultExpected, SizeChecker sut)
        {
            var leftBytes = BinaryDataWriter.Write(leftData);
            var rightBytes = BinaryDataWriter.Write(rightData);

            sut.DiffChecker.Check(leftBytes, rightBytes).Returns(new DiffResult());

            var result = sut.Check(leftBytes, rightBytes);

            result.SameSize.Should().Be(resultExpected);
        }        
    }
}
