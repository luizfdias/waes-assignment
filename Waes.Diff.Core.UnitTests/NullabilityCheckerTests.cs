using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using Waes.Diff.Core.Exceptions;
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
        public void Check_WhenNullParams_ShouldThrowDataNotFoundException(NullabilityChecker sut)
        {
            Assert.Throws<DataNotFoundException>(() => sut.Check(null, null));
        }

        [Theory, AutoNSubstituteData]
        public void Check_WhenFirstParamsIsNull_ShouldThrowDataNotFoundException(NullabilityChecker sut)
        {
            Assert.Throws<DataNotFoundException>(() => sut.Check(null, new Data()));
        }

        [Theory, AutoNSubstituteData]
        public void Check_WhenSecondParamsIsNull_ShouldThrowDataNotFoundException(NullabilityChecker sut)
        {
            Assert.Throws<DataNotFoundException>(() => sut.Check(new Data(), null));
        }

        [Theory, AutoNSubstituteData]
        public void Check_WhenAllDataIsProvided_ShouldReturnDiffResultAsExpected(NullabilityChecker sut, Data leftData, Data rightData, DiffResult diffResult)
        {
            sut.DiffChecker.Check(leftData, rightData).Returns(diffResult);

            var result = sut.Check(leftData, rightData);

            result.Should().Be(diffResult);
        }
    }
}
