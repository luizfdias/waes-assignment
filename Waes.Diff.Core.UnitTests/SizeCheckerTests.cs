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
        public void Check_WhenBinaryDataHasSameSizeAndAreEqual_SameSizePropertyShouldBeTrue(SizeChecker sut)
        {
            var leftBytes = BinaryDataWriter.Write("abc123");
            var rightBytes = BinaryDataWriter.Write("abc123");

            sut.DiffChecker.Check(leftBytes, rightBytes).Returns(new DiffResult { SameSize = true });

            var result = sut.Check(leftBytes, rightBytes);

            result.SameSize.Should().BeTrue();
        }

        [Theory, AutoNSubstituteData]
        public void Check_WhenBinaryDataHasSameSizeAndAreDifferents_SameSizePropertyShouldBeTrue(SizeChecker sut)
        {
            var leftBytes = BinaryDataWriter.Write("abc123");
            var rightBytes = BinaryDataWriter.Write("XYZ987");

            sut.DiffChecker.Check(leftBytes, rightBytes).Returns(new DiffResult { SameSize = true });

            var result = sut.Check(leftBytes, rightBytes);

            result.SameSize.Should().BeTrue();
        }

        [Theory, AutoNSubstituteData]
        public void Check_WhenBinaryDataHasDifferentSizes_SameSizePropertyShouldBeFalse(SizeChecker sut)
        {
            var leftBytes = BinaryDataWriter.Write("abc1234");
            var rightBytes = BinaryDataWriter.Write("abc123");

            sut.DiffChecker.Check(leftBytes, rightBytes).Returns(new DiffResult { SameSize = true });

            var result = sut.Check(leftBytes, rightBytes);

            result.SameSize.Should().BeFalse();
        }
    }
}
