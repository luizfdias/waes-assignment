using FluentAssertions;
using System.Linq;
using Waes.Assignment.Domain.Services;
using Xunit;

namespace Waes.Assignment.UnitTests.Domain
{
    public class DifferenceIntervalFinderTests
    {
        private readonly DifferenceIntervalFinder _sut;

        public DifferenceIntervalFinderTests()
        {
            _sut = new DifferenceIntervalFinder();
        }

        [Fact]
        public void Find_WhenMultipleDifferences_ShouldReturnStartIndexAndLengthOfIt()
        {
            var indexOfDifferences = new int[] { 1, 2, 3, 5, 6, 10, 13, 14, 15, 19, 20 };

            var result = _sut.Find(indexOfDifferences).ToArray();

            result[0].StartIndex.Should().Be(0);
            result[0].Length.Should().Be(3);

            result[1].StartIndex.Should().Be(3);
            result[1].Length.Should().Be(2);

            result[2].StartIndex.Should().Be(5);
            result[2].Length.Should().Be(1);

            result[3].StartIndex.Should().Be(6);
            result[3].Length.Should().Be(3);

            result[4].StartIndex.Should().Be(9);
            result[4].Length.Should().Be(2);
        }

        [Fact]
        public void Find_WhenItHasOneDifferenceOfLengthOne_ShouldReturnStartIndexAndLengthOfIt()
        {
            var indexOfDifferences = new int[] { 1 };

            var result = _sut.Find(indexOfDifferences).ToArray();

            result[0].StartIndex.Should().Be(0);
            result[0].Length.Should().Be(1);
        }

        [Fact]
        public void Find_WhenItHasOneDifferenceOfLengthBiggerThanOne_ShouldReturnStartIndexAndLengthOfIt()
        {
            var indexOfDifferences = new int[] {1, 2, 3 };

            var result = _sut.Find(indexOfDifferences).ToArray();

            result[0].StartIndex.Should().Be(0);
            result[0].Length.Should().Be(3);
        }

        [Fact]
        public void Find_WhenThereIsNoDifferences_ShouldReturnAnEmptyList()
        {
            var indexOfDifferences = new int[] { };

            var result = _sut.Find(indexOfDifferences).ToArray();

            result.Should().BeEmpty();
        }
    }
}
