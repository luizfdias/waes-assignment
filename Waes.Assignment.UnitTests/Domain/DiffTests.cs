using FluentAssertions;
using System.Linq;
using Waes.Assignment.Domain.Models.Enums;
using Waes.Assignment.Domain.ValueObjects;
using Xunit;

namespace Waes.Assignment.UnitTests.Domain
{
    public class DiffTests
    {
        [Fact]
        public void GetSequenceOfDifferences_WhenMultipleDifferences_MustReturnStartIndexAndLengthOfIt()
        {
            var diff = new DiffInfo(DiffStatus.NotEqual, new DiffPosition[]
            {
                new DiffPosition(1),
                new DiffPosition(2),
                new DiffPosition(3),
                new DiffPosition(5),
                new DiffPosition(6),
                new DiffPosition(10),
                new DiffPosition(13),
                new DiffPosition(14),
                new DiffPosition(15),
                new DiffPosition(19),
                new DiffPosition(20),
            });

            var result = diff.GetSequenceOfDifferences().ToArray();

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
        public void GetSequenceOfDifferences_WhenItHasOneDifferenceOfLengthOne_MustReturnStartIndexAndLengthOfIt()
        {
            var diff = new DiffInfo(DiffStatus.NotEqual, new DiffPosition[]
            {
                new DiffPosition(1)
            });

            var result = diff.GetSequenceOfDifferences().ToArray();

            result[0].StartIndex.Should().Be(0);
            result[0].Length.Should().Be(1);
        }

        [Fact]
        public void GetSequenceOfDifferences_WhenItHasOneDifferenceOfLengthBiggerThanOne_MustReturnStartIndexAndLengthOfIt()
        {
            var diff = new DiffInfo(DiffStatus.NotEqual, new DiffPosition[]
            {
                new DiffPosition(1),
                new DiffPosition(2),
                new DiffPosition(3),
            });

            var result = diff.GetSequenceOfDifferences().ToArray();

            result[0].StartIndex.Should().Be(0);
            result[0].Length.Should().Be(3);
        }

        [Fact]
        public void GetSequenceOfDifferences_WhenNotOfEqualSize_MustReturnAnEmptyList()
        {
            var diff = new DiffInfo(DiffStatus.NotOfEqualSize);

            var result = diff.GetSequenceOfDifferences();

            result.Should().BeEmpty();       
        }

        [Fact]
        public void GetSequenceOfDifferences_WhenEqual_MustReturnAnEmptyList()
        {
            var diff = new DiffInfo(DiffStatus.Equal);

            var result = diff.GetSequenceOfDifferences();

            result.Should().BeEmpty();
        }
    }
}
