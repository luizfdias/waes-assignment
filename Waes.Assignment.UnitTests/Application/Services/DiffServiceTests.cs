using AutoFixture.Idioms;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Waes.Assignment.Application.Services;
using Waes.Assignment.Application.ApiModels;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;
using Waes.Assignment.Application.Interfaces;

namespace Waes.Assignment.UnitTests.Application.Services
{
    public class DiffServiceTests
    {
        private readonly ICache _cache;

        private readonly IMapper _mapper;

        private readonly DiffService _sut;

        public DiffServiceTests()
        {
            _cache = Substitute.For<ICache>();
            _mapper = Substitute.For<IMapper>();

            _sut = new DiffService(_cache, _mapper);
        }

        [Theory, AutoNSubstituteData]
        public void Constructor_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(DiffService).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async void Get_WhenDiffIsNotFound_ShouldReturnNull(string correlationId)
        {
            _cache.GetAsync<Diff>(Arg.Any<string>()).ReturnsNull();
            _mapper.Map<DiffResponse>(Arg.Any<Diff>()).ReturnsNull();

            var result = await _sut.Get(correlationId);

            result.Should().BeNull();
        }

        [Theory, AutoNSubstituteData]
        public async void Get_WhenDiffIsFound_ShouldReturnDiffResponse(string correlationId, EqualDiff diff, DiffResponse diffResponse)
        {
            _cache.GetAsync<Diff>($"diff_{correlationId}").Returns(diff);

            _mapper.Map<DiffResponse>(diff).Returns(diffResponse);

            var result = await _sut.Get(correlationId);

            result.Should().Be(diffResponse);            
        }
    }
}
