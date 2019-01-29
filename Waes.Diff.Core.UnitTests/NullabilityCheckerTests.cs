using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using Waes.Diff.Core.Models;
using Waes.Diff.Core.UnitTests.AutoData;
using Xunit;

namespace Waes.Diff.Core.UnitTests
{
    public class NullabilityCheckerTests
    {
        [Theory, AutoNSubstituteData]
        public void Constructors_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(NullabilityChecker).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public void Check_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(NullabilityChecker).GetMethods());            
        }

        [Theory, AutoNSubstituteData]
        public void Check_WhenAllDataIsProvided_ShouldReturnDiffResultAsExpected(NullabilityChecker sut, byte[] leftData, byte[] rightData, DiffResult diffResult)
        {
            sut.DiffChecker.Check(leftData, rightData).Returns(diffResult);

            var result = sut.Check(leftData, rightData);

            result.Should().Be(diffResult);
        }
    }
}
