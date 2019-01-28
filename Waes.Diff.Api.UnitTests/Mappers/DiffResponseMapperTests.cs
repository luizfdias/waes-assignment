using AutoFixture.Idioms;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Waes.Diff.Api.Mappers;
using Waes.Diff.Api.UnitTests.AutoData;
using Waes.Diff.Core.Models;
using Xunit;

namespace Waes.Diff.Api.UnitTests.Mappers
{
    public class DiffResponseMapperTests
    {
        [Theory, AutoNSubstituteData]
        public void Constructors_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(DiffResponseMapper).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public void Map_WhenDiffResultHasAllData_ShouldMapAsExpected(DiffResponseMapper sut, DiffResult diffResult)
        {
            var diffResponse = sut.Map(diffResult);

            diffResponse.EqualsSize.Should().Be(diffResult.SameSize);

            diffResponse.DataInfo.Should().HaveCount(2);

            diffResponse.DataInfo.ToList()[0].Id.Should().Be(diffResult.LeftDataInfo.Id);
            diffResponse.DataInfo.ToList()[0].Length.Should().Be(diffResult.LeftDataInfo.Length);

            diffResponse.DataInfo.ToList()[1].Id.Should().Be(diffResult.RightDataInfo.Id);
            diffResponse.DataInfo.ToList()[1].Length.Should().Be(diffResult.RightDataInfo.Length);

            diffResponse.Differences.Should().HaveCount(diffResult.Differences.Count);

            diffResponse.Differences.First().Length.Should().Be(diffResult.Differences.First().Length);
            diffResponse.Differences.First().StartOffSet.Should().Be(diffResult.Differences.First().StartOffSet);
        }

        [Theory, AutoNSubstituteData]
        public void Map_WhenDifferencesIsEmpty_ShouldReturnDifferencesNull(DiffResponseMapper sut, DiffResult diffResult)
        {
            diffResult.Differences = new List<Difference>();

            var result = sut.Map(diffResult);

            result.Differences.Should().BeNull();
        }
    }
}
