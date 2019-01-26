using FluentAssertions;
using System.Linq;
using Waes.Diff.Core.UnitTests.AutoData;
using Waes.Diff.Core.UnitTests.Helpers;
using Xunit;

namespace Waes.Diff.Core.UnitTests
{
    public class BytesCheckerTests
    {
        [Theory, AutoNSubstituteData]
        public void Check_WhenBinaryDataHaveSameSizeAndAreEqual_DiffsShouldBeEmpty(BytesChecker sut)
        {
            var leftBytes = BinaryDataWriter.Write("abc123");
            var rightBytes = BinaryDataWriter.Write("abc123");

            var result = sut.Check(leftBytes, rightBytes);

            result.Diffs.Should().HaveCount(0);
        }

        [Theory, AutoNSubstituteData]
        public void Check_WhenBinaryDataHaveSameSizeAndAreDifferents_ShouldReturnTheStartOffSetAndLengthOfIt(BytesChecker sut)
        {
            var leftBytes = BinaryDataWriter.Write("abc123");
            var rightBytes = BinaryDataWriter.Write("XYZ987");

            var result = sut.Check(leftBytes, rightBytes);

            result.Diffs.Should().HaveCount(1);
            result.Diffs.FirstOrDefault().StartOffSet.Should().Be(1);
            result.Diffs.FirstOrDefault().Length.Should().Be(6);
        }

        [Theory, AutoNSubstituteData]
        public void Check_WhenBinaryDataHaveSameSizeAndHaveManyDifferences_ShouldReturnResultAsExpected(BytesChecker sut)
        {
            var leftBytes = BinaryDataWriter.Write("abc123 xyz qwer");
            var rightBytes = BinaryDataWriter.Write("abc123 xrz q   ");

            var result = sut.Check(leftBytes, rightBytes);

            result.Diffs.Should().HaveCount(2);
            result.Diffs.FirstOrDefault().StartOffSet.Should().Be(9);
            result.Diffs.FirstOrDefault().Length.Should().Be(1);

            result.Diffs.ToList()[1].StartOffSet.Should().Be(13);
            result.Diffs.ToList()[1].Length.Should().Be(3);
        }
    }
}
