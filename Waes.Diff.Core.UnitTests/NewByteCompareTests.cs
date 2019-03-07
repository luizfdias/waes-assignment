using FluentAssertions;
using System.Linq;
using Waes.Assignment.Domain.Algorithms;
using Waes.Assignment.Domain.Models;
using Xunit;

namespace Waes.Assignment.Domain.UnitTests
{
    public class NewByteCompareTests
    {
        #region newChecker
        [Fact]
        public void WhenDataIsEmpty_ShouldReturnEqual()
        {
            var a1 = new PayLoad(new byte[] { });
            var a2 = new PayLoad(new byte[] { });

            var sut = new RecursionDiffEngine();

            var result = sut.ProcessDiff(a1, a2);

            result.Should().HaveCount(0);
        }

        [Fact]
        public void WhenArraysAreNotEqual_ShouldReturnEqual()
        {
            var a1 = new PayLoad(new byte[] { 1, 2, 3, 6 });
            var a2 = new PayLoad(new byte[] { 1, 4, 5, 6 });

            var sut = new DiffAlgorithmWithRecursion();

            var result = sut.ProcessDiff(a1, a2);

            result.Should().HaveCount(1);
            result.ToArray()[0].StartIndex.Should().Be(1);
            result.ToArray()[0].Length.Should().Be(2);
        }

        [Fact]
        public void WhenArraysAreEqual_ShouldReturnEqual()
        {
            var a1 = new PayLoad(new byte[] { 1, 2, 3});
            var a2 = new PayLoad(new byte[] { 1, 2, 3});

            var sut = new DiffAlgorithmWithRecursion();

            var result = sut.ProcessDiff(a1, a2);

            result.Should().HaveCount(0);
        }

        [Fact]
        public void WhenArraysAreNotEqual2_ShouldReturnEqual()
        {
            var a1 = new PayLoad(new byte[] { 1, 2, 3, 3 });
            var a2 = new PayLoad(new byte[] { 1, 4, 5, 6 });

            var sut = new DiffAlgorithmWithRecursion();

            var result = sut.ProcessDiff(a1, a2);

            result.Should().HaveCount(1);
            result.ToArray()[0].StartIndex.Should().Be(1);
            result.ToArray()[0].Length.Should().Be(3);
        }

        [Fact]
        public void WhenArraysAreNotEqual3_ShouldReturnEqual()
        {
            var a1 = new PayLoad(new byte[] { 3, 2, 2, 2 });
            var a2 = new PayLoad(new byte[] { 1, 2, 2, 2 });

            var sut = new DiffAlgorithmWithRecursion();

            var result = sut.ProcessDiff(a1, a2);

            result.Should().HaveCount(1);
            result.ToArray()[0].StartIndex.Should().Be(0);
            result.ToArray()[0].Length.Should().Be(1);
        }

        [Fact]
        public void WhenArraysAreNotEqual4_ShouldReturnEqual()
        {
            var a1 = new PayLoad(new byte[] { 1, 2, 3, 1, 59, 5 });
            var a2 = new PayLoad(new byte[] { 1, 4, 5, 1, 27, 5 });

            var sut = new DiffAlgorithmWithRecursion();

            var result = sut.ProcessDiff(a1, a2);

            result.Should().HaveCount(2);
            result.ToArray()[0].StartIndex.Should().Be(1);
            result.ToArray()[0].Length.Should().Be(2);

            result.ToArray()[1].StartIndex.Should().Be(4);
            result.ToArray()[1].Length.Should().Be(1);
        }

        [Fact]
        public void WhenArraysAreNotEqual5_ShouldReturnEqual()
        {
            var a1 = new PayLoad(new byte[] { 1, 2, 3, 1, 59, 5 });
            var a2 = new PayLoad(new byte[] { 1, 4, 5, 1, 27, 5 });
            
            for (int i = 0; i < 500000; i++)
            {
                var sut = new RecursionDiffEngine();

                var result = sut.ProcessDiff(a1, a2);
            }
        }

        #endregion

        #region newIteratorChecker
        [Fact]
        public void C_WhenDataIsEmpty_ShouldReturnEqual()
        {
            var a1 = new PayLoad(new byte[] { });
            var a2 = new PayLoad(new byte[] { });

            var sut = new DiffAlgorithmWithIteration();

            var result = sut.ProcessDiff(a1, a2);

            result.Should().HaveCount(0);
        }

        [Fact]
        public void C_WhenArraysAreNotEqual_ShouldReturnEqual()
        {
            var a1 = new PayLoad(new byte[] { 1, 2, 3, 6 });
            var a2 = new PayLoad(new byte[] { 1, 4, 5, 6 });

            var sut = new DiffAlgorithmWithIteration();

            var result = sut.ProcessDiff(a1, a2);

            result.Should().HaveCount(1);            

            result.ToArray()[0].StartIndex.Should().Be(1);
            result.ToArray()[0].Length.Should().Be(2);
        }

        [Fact]
        public void C_WhenArraysAreEqual_ShouldReturnEqual()
        {
            var a1 = new PayLoad(new byte[] { 1, 2, 3 });
            var a2 = new PayLoad(new byte[] { 1, 2, 3 });

            var sut = new DiffAlgorithmWithIteration();

            var result = sut.ProcessDiff(a1, a2);

            result.Should().HaveCount(0);
        }

        [Fact]
        public void C_WhenArraysAreNotEqual2_ShouldReturnEqual()
        {
            var a1 = new PayLoad(new byte[] { 1, 2, 3, 3 });
            var a2 = new PayLoad(new byte[] { 1, 4, 5, 6 });

            var sut = new DiffAlgorithmWithIteration();

            var result = sut.ProcessDiff(a1, a2);

            result.Should().HaveCount(1);
            result.ToArray()[0].StartIndex.Should().Be(1);
            result.ToArray()[0].Length.Should().Be(3);
        }

        [Fact]
        public void C_WhenArraysAreNotEqual3_ShouldReturnEqual()
        {
            var a1 = new PayLoad(new byte[] { 3, 2, 2, 2 });
            var a2 = new PayLoad(new byte[] { 1, 2, 2, 2 });

            var sut = new DiffAlgorithmWithIteration();

            var result = sut.ProcessDiff(a1, a2);

            result.Should().HaveCount(1);
            result.ToArray()[0].StartIndex.Should().Be(0);
            result.ToArray()[0].Length.Should().Be(1);
        }

        [Fact]
        public void C_WhenArraysAreNotEqual4_ShouldReturnEqual()
        {
            var a1 = new PayLoad(new byte[] { 1, 2, 3, 1, 59, 5 });
            var a2 = new PayLoad(new byte[] { 1, 4, 5, 1, 27, 5 });

            var sut = new DiffAlgorithmWithIteration();

            var result = sut.ProcessDiff(a1, a2);

            result.Should().HaveCount(2);
            result.ToArray()[0].StartIndex.Should().Be(1);
            result.ToArray()[0].Length.Should().Be(2);

            result.ToArray()[1].StartIndex.Should().Be(4);
            result.ToArray()[1].Length.Should().Be(1);
        }

        [Fact]
        public void C_WhenArraysAreNotEqual5_ShouldReturnEqual()
        {
            var a1 = new PayLoad(new byte[] { 1, 2, 3, 1, 59, 5 });
            var a2 = new PayLoad(new byte[] { 1, 4, 5, 1, 27, 5 });

            for (int i = 0; i < 500000; i++)
            {
                var sut = new DiffAlgorithmWithIteration();

                var result = sut.ProcessDiff(a1, a2);
            }
        }

        
        #endregion
    }
}
