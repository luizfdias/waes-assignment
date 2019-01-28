using AutoFixture.Idioms;
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
        public void Constructors_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(BytesChecker).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public void Check_WhenEquals_DiffsShouldBeEmpty(BytesChecker sut)
        {
            var leftData = BinaryDataWriter.Write("abc123");
            var rightData = BinaryDataWriter.Write("abc123");

            var result = sut.Check(leftData, rightData);

            result.Differences.Should().BeEmpty();
        }

        [Theory, AutoNSubstituteData]
        public void Check_WhenSameSizeAndDifferents_ShouldReturnTheStartOffSetAndLengthOfIt(BytesChecker sut)
        {
            var leftData = BinaryDataWriter.Write("abc123");
            var rightData = BinaryDataWriter.Write("XYZ987");

            var result = sut.Check(leftData, rightData);

            result.Differences.Should().HaveCount(1);
            result.Differences.FirstOrDefault().StartOffSet.Should().Be(1);
            result.Differences.FirstOrDefault().Length.Should().Be(6);
        }

        [Theory, AutoNSubstituteData]
        public void Check_WhenSameSizeAndManyDifferences_ShouldReturnResultAsExpected(BytesChecker sut)
        {
            var leftData = BinaryDataWriter.Write("abc123 xyz qwer");
            var rightData = BinaryDataWriter.Write("abc123 xrz q   ");

            var result = sut.Check(leftData, rightData);

            result.Differences.Should().HaveCount(2);
            result.Differences.FirstOrDefault().StartOffSet.Should().Be(9);
            result.Differences.FirstOrDefault().Length.Should().Be(1);

            result.Differences.ToList()[1].StartOffSet.Should().Be(13);
            result.Differences.ToList()[1].Length.Should().Be(3);
        }

        [Theory, AutoNSubstituteData]
        public void Check_WhenEmpty_DiffsShouldBeEmpty(BytesChecker sut)
        {
            var leftData = BinaryDataWriter.Write("");
            var rightData = BinaryDataWriter.Write("");

            var result = sut.Check(leftData, rightData);

            result.Differences.Should().BeEmpty();
        }
    }
}
