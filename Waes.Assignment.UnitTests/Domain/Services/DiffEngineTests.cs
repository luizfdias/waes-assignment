using FluentAssertions;
using NSubstitute;
using System.Collections.Generic;
using System.Text;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Domain.Services;
using Waes.Assignment.Domain.ValueObjects;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Domain.Services
{
    public class DiffEngineTests
    {
        private readonly IDifferenceIntervalFinder _differenceIntervalFinder;

        private readonly DiffEngine _sut;

        public DiffEngineTests()
        {
            _differenceIntervalFinder = Substitute.For<IDifferenceIntervalFinder>();

            _sut = new DiffEngine(_differenceIntervalFinder);
        }

        [Theory, AutoNSubstituteData]
        public void ProcessDiff_WhenBytesContentAreEqual_MustReturnEqual(string correlationId)
        {
            var left = Encoding.ASCII.GetBytes("abc123");
            var right = Encoding.ASCII.GetBytes("abc123");

            var result = _sut.ProcessDiff(correlationId, left, right);

            result.Should().BeOfType<EqualDiff>();
        }

        [Theory, AutoNSubstituteData]
        public void ProcessDiff_WhenBytesContentAreNotEqual_MustReturnNotEqual(string correlationId, 
            IEnumerable<DifferenceInterval> differenceIntervals)
        {
            _differenceIntervalFinder.Find(Arg.Any<int[]>()).Returns(differenceIntervals);

            var left = Encoding.ASCII.GetBytes("abc123");
            var right = Encoding.ASCII.GetBytes("aYZ123");

            var result = _sut.ProcessDiff(correlationId, left, right);

            result.Should().BeOfType<NotEqualDiff>();
            ((NotEqualDiff)result).Differences.Should().BeEquivalentTo(differenceIntervals);
        }

        [Theory, AutoNSubstituteData]
        public void ProcessDiff_WhenBytesContentAreNotOfEqualSize_MustReturnNotOfEqualSize(string correlationId)
        {
            var left = Encoding.ASCII.GetBytes("abc123");
            var right = Encoding.ASCII.GetBytes("abc12345");

            var result = _sut.ProcessDiff(correlationId, left, right);

            result.Should().BeOfType<NotOfEqualSizeDiff>();
        }
    }
}
