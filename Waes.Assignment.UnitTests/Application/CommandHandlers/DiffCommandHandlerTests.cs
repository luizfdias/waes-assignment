using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using System.Collections.Generic;
using System.Threading;
using Waes.Assignment.Application.CommandHandlers;
using Waes.Assignment.Domain.Commands;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Domain.Models.Enums;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Application.CommandHandlers
{
    public class DiffCommandHandlerTests
    {
        private readonly IMediatorHandler _bus;

        private readonly IPayLoadRepository _payLoadRepository;

        private readonly IDiffDomainService _diffDomainService;

        private readonly IDiffRepository _diffRepository;

        private readonly DiffCommandHandler _sut;

        public DiffCommandHandlerTests()
        {
            _bus = Substitute.For<IMediatorHandler>();
            _payLoadRepository = Substitute.For<IPayLoadRepository>();
            _diffDomainService = Substitute.For<IDiffDomainService>();
            _diffRepository = Substitute.For<IDiffRepository>();

            _sut = new DiffCommandHandler(_bus, _diffDomainService, _payLoadRepository, _diffRepository);
        }

        [Theory, AutoNSubstituteData]
        public void Constructor_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(DiffCommandHandler).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async void Handle_WhenLeftPayLoadIsNotFound_ShouldReturnFalse(AnalyzeDiffCommand command, byte[] content)
        {
            var payLoads = new List<PayLoad>
            {
                new PayLoad(command.CorrelationId, content, SideEnum.Right)
            };

            _payLoadRepository.GetByCorrelationId(command.CorrelationId).Returns(payLoads);

            var result = await _sut.Handle(command, new CancellationToken());

            result.Should().BeFalse();
        }

        [Theory, AutoNSubstituteData]
        public async void Handle_WhenRightPayLoadIsNotFound_ShouldReturnFalse(AnalyzeDiffCommand command, byte[] content)
        {
            var payLoads = new List<PayLoad>
            {
                new PayLoad(command.CorrelationId, content, SideEnum.Left)
            };

            _payLoadRepository.GetByCorrelationId(command.CorrelationId).Returns(payLoads);

            var result = await _sut.Handle(command, new CancellationToken());

            result.Should().BeFalse();
        }

        [Theory, AutoNSubstituteData]
        public async void Handle_WhenPayLoadIsFound_ShouldReturnTrue(AnalyzeDiffCommand command, byte[] content)
        {
            var payLoads = new List<PayLoad>
            {
                new PayLoad(command.CorrelationId, content, SideEnum.Left),
                new PayLoad(command.CorrelationId, content, SideEnum.Right)
            };

            _payLoadRepository.GetByCorrelationId(command.CorrelationId).Returns(payLoads);

            var result = await _sut.Handle(command, new CancellationToken());

            result.Should().BeTrue();
        }
    }
}
