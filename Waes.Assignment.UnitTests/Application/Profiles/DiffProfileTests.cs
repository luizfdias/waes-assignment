using AutoMapper;
using FluentAssertions;
using Waes.Assignment.Api.ViewModels;
using Waes.Assignment.Application.ViewModels;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Application.Profiles
{
    public class DiffProfileTests
    {
        [Theory, AutoNSubstituteDataAutoMapper]
        public void Map_WhenMappingFromEqualDiffToDiffResponse_ShouldMapAsExpected(IMapper sut, EqualDiff equalDiff)
        {
            Diff diff = equalDiff;

            var result = sut.Map<DiffResponse>(diff);

            result.Should().BeOfType<EqualResponse>();
        }

        [Theory, AutoNSubstituteDataAutoMapper]
        public void Map_WhenMappingFromNotOfEqualSizeDiffToDiffResponse_ShouldMapAsExpected(IMapper sut, NotOfEqualSizeDiff notOfEqualSizeDiff)
        {
            Diff diff = notOfEqualSizeDiff;

            var result = sut.Map<DiffResponse>(diff);

            result.Should().BeOfType<NotOfEqualSizeResponse>();
        }

        [Theory, AutoNSubstituteDataAutoMapper]
        public void Map_WhenMappingFromNotEqualDiffToDiffResponse_ShouldMapAsExpected(IMapper sut, NotEqualDiff notEqualDiff)
        {
            Diff diff = notEqualDiff;

            var result = sut.Map<DiffResponse>(diff);

            result.Should().BeOfType<NotEqualResponse>();
            var notEqualResponse = (NotEqualResponse)result;
            notEqualResponse.Info.Should().BeEquivalentTo(notEqualDiff.Differences);
        }
    }
}
