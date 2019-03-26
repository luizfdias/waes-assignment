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
        public void Map_WhenMappingFromDiffSequenceToDiffResponse_ResultShouldBeAsExpected(DiffSequence diffSequence)
        {
            var result = Mapper.Map<DiffInfoResponse>(diffSequence);

            result.Length.Should().Be(diffSequence.Length);
            result.StartIndex.Should().Be(diffSequence.StartIndex);
        }
    }
}
