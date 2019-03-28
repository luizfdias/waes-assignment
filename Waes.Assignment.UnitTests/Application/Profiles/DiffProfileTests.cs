using AutoMapper;
using FluentAssertions;
using Waes.Assignment.Application.ViewModels;
using Waes.Assignment.Domain.ValueObjects;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Application.Profiles
{
    [Collection("Automapper collection")]
    public class DiffProfileTests
    {
        [Theory, AutoNSubstituteData]
        public void Map_WhenMappingFromDiffSequenceToDiffResponse_ResultShouldBeAsExpected(Differences differences)
        {
            var result = Mapper.Map<DiffInfoResponse>(differences);

            result.Length.Should().Be(differences.Length);
            result.StartIndex.Should().Be(differences.StartIndex);
        }
    }
}
