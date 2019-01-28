using AutoFixture.Idioms;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Waes.Diff.Api.Contracts;
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
        public void Map_WhenNotEqualOfSize_ShouldMapAsExpected(DiffResponseMapper sut, DiffResult diffResult)
        {
            diffResult.SameSize = false;

            var diffResponse = sut.Map(diffResult);

            diffResponse.Code.Should().Be(ApiReturns.NotOfEqualSize.Code);
            diffResponse.Message.Should().Be(ApiReturns.NotOfEqualSize.Message);

            diffResponse.DataInfo.Should().HaveCount(2);

            diffResponse.DataInfo.ToList()[0].Id.Should().Be(diffResult.LeftDataInfo.Id);
            diffResponse.DataInfo.ToList()[0].Length.Should().Be(diffResult.LeftDataInfo.Length);

            diffResponse.DataInfo.ToList()[1].Id.Should().Be(diffResult.RightDataInfo.Id);
            diffResponse.DataInfo.ToList()[1].Length.Should().Be(diffResult.RightDataInfo.Length);

            diffResponse.Differences.Should().BeNull();            
        }

        [Theory, AutoNSubstituteData]
        public void Map_WhenEqual_ShouldMapAsExpected(DiffResponseMapper sut, DiffResult diffResult)
        {
            diffResult.SameSize = true;
            diffResult.Differences = new List<Core.Models.Difference>();

            var diffResponse = sut.Map(diffResult);

            diffResponse.Code.Should().Be(ApiReturns.Equal.Code);
            diffResponse.Message.Should().Be(ApiReturns.Equal.Message);

            diffResponse.DataInfo.Should().HaveCount(2);

            diffResponse.DataInfo.ToList()[0].Id.Should().Be(diffResult.LeftDataInfo.Id);
            diffResponse.DataInfo.ToList()[0].Length.Should().Be(diffResult.LeftDataInfo.Length);

            diffResponse.DataInfo.ToList()[1].Id.Should().Be(diffResult.RightDataInfo.Id);
            diffResponse.DataInfo.ToList()[1].Length.Should().Be(diffResult.RightDataInfo.Length);

            diffResponse.Differences.Should().BeNull();
        }

        [Theory, AutoNSubstituteData]
        public void Map_WhenNotEqual_ShouldMapAsExpected(DiffResponseMapper sut, DiffResult diffResult)
        {
            diffResult.SameSize = true;

            var diffResponse = sut.Map(diffResult);

            diffResponse.Code.Should().Be(ApiReturns.NotEqual.Code);
            diffResponse.Message.Should().Be(ApiReturns.NotEqual.Message);

            diffResponse.DataInfo.Should().HaveCount(2);

            diffResponse.DataInfo.ToList()[0].Id.Should().Be(diffResult.LeftDataInfo.Id);
            diffResponse.DataInfo.ToList()[0].Length.Should().Be(diffResult.LeftDataInfo.Length);

            diffResponse.DataInfo.ToList()[1].Id.Should().Be(diffResult.RightDataInfo.Id);
            diffResponse.DataInfo.ToList()[1].Length.Should().Be(diffResult.RightDataInfo.Length);

            diffResponse.Differences.Should().HaveCount(diffResult.Differences.Count);

            diffResponse.Differences.First().Length.Should().Be(diffResult.Differences.First().Length);
            diffResponse.Differences.First().StartOffSet.Should().Be(diffResult.Differences.First().StartOffSet);
        }
    }
}
