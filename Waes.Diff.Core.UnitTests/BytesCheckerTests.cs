using AutoFixture.Idioms;
using FluentAssertions;
using System.Linq;
using Waes.Diff.Core.Factories;
using Waes.Diff.Core.UnitTests.AutoData;
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
        public void Check_WhenEqual_DiffsShouldBeEmpty(BytesChecker sut)
        {
            var leftData = DataFactory.Create(new byte[] { 1, 2, 3 }, "abc123", Models.SideEnum.Left);
            var rightData = DataFactory.Create(new byte[] { 1, 2, 3 }, "abc123", Models.SideEnum.Right);

            var result = sut.Check(leftData, rightData);

            result.Differences.Should().BeEmpty();
        }

        [Theory, AutoNSubstituteData]
        public void Check_WhenNotEqual_ShouldReturnTheStartOffSetAndLengthOfIt(BytesChecker sut)
        {
            var leftData = DataFactory.Create(new byte[] { 1, 7, 8 }, "abc123", Models.SideEnum.Left);
            var rightData = DataFactory.Create(new byte[] { 1, 2, 3 }, "abc123", Models.SideEnum.Right);

            var result = sut.Check(leftData, rightData);

            result.Differences.Should().HaveCount(1);
            result.Differences.FirstOrDefault().StartOffSet.Should().Be(1);
            result.Differences.FirstOrDefault().Length.Should().Be(2);
        }

        [Theory, AutoNSubstituteData]
        public void Check_WhenNotEqualWithManyDifferences_ShouldReturnResultAsExpected(BytesChecker sut)
        {
            var leftData = DataFactory.Create(new byte[] { 1, 8, 3, 5, 5 }, "abc123", Models.SideEnum.Left);
            var rightData = DataFactory.Create(new byte[] { 1, 2, 3, 4, 3 }, "abc123", Models.SideEnum.Right);

            var result = sut.Check(leftData, rightData);

            result.Differences.Should().HaveCount(2);
            result.Differences.FirstOrDefault().StartOffSet.Should().Be(1);
            result.Differences.FirstOrDefault().Length.Should().Be(1);

            result.Differences.ToList()[1].StartOffSet.Should().Be(3);
            result.Differences.ToList()[1].Length.Should().Be(2);
        }

        [Theory, AutoNSubstituteData]
        public void Check_WhenContentIsEmpty_DiffsShouldBeEmpty(BytesChecker sut)
        {
            var leftData = DataFactory.Create(new byte[] { }, "abc123", Models.SideEnum.Left);
            var rightData = DataFactory.Create(new byte[] { }, "abc123", Models.SideEnum.Right);

            var result = sut.Check(leftData, rightData);

            result.Differences.Should().BeEmpty();
        }
    }
}
