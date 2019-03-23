using AutoFixture.Idioms;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Waes.Assignment.Application.Services;
using Waes.Assignment.Application.ViewModels;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Application.Services
{
    public class DiffServiceTests
    {
        private readonly IDiffRepository _diffRepository;

        private readonly IMapper _mapper;

        private readonly IMediatorHandler _mediatorHandler;

        private readonly DiffService _diffService;

        public DiffServiceTests()
        {
            _diffRepository = Substitute.For<IDiffRepository>();
            _mapper = Substitute.For<IMapper>();
            _mediatorHandler = Substitute.For<IMediatorHandler>();

            _diffService = new DiffService(_diffRepository, _mapper, _mediatorHandler);
        }

        [Theory, AutoNSubstituteData]
        public void Constructor_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(DiffService).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async void Get_WhenDiffIsNotFound_ShouldReturnNull(string correlationId)
        {
            _diffRepository.GetByCorrelationId(Arg.Any<string>()).ReturnsNull();
            
            var result = await _diffService.Get(correlationId);

            result.Should().BeNull();
        }

        [Theory, AutoNSubstituteData]
        public async void Get_WhenDiffIsFound_ShouldReturnDiffResponse(string correlationId, Diff diff, DiffResponse diffResponse)
        {
            _diffRepository.GetByCorrelationId(correlationId).Returns(diff);

            _mapper.Map<DiffResponse>(diff).Returns(diffResponse);

            var result = await _diffService.Get(correlationId);

            result.Should().Be(diffResponse);            
        }
    }
}
