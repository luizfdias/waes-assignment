using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using System;
using System.Threading;
using Waes.Assignment.Application.CommandHandlers;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Domain.Commands;
using Waes.Assignment.Domain.Events;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Application.CommandHandlers
{
    public class PayLoadCommandHandlerTests
    {
        private readonly IMediatorHandler _bus;

        private readonly IPayLoadRepository _payLoadRepository;

        private readonly PayLoadCommandHandler _sut;

        public PayLoadCommandHandlerTests()
        {
            _bus = Substitute.For<IMediatorHandler>();
            _payLoadRepository = Substitute.For<IPayLoadRepository>();

            _sut = new PayLoadCommandHandler(_bus, _payLoadRepository);
        }

        [Theory, AutoNSubstituteData]
        public void Constructor_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(PayLoadCommandHandler).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async void Handle_WhenPayLoadNotExist_ShouldAddItToRepository(PayLoadCreateCommand command)
        {
            var result = await _sut.Handle(command, new CancellationToken());

            result.Should().BeTrue();

            await _payLoadRepository.Received(1).Add(Arg.Is<PayLoad>(p
                => p.CorrelationId == command.CorrelationId &&
                p.Content == command.Content &&
                p.Side == command.Side));
        }

        [Theory, AutoNSubstituteData]
        public async void Handle_WhenPayLoadCreated_ShouldRaisePayLoadCreatedEvent(PayLoadCreateCommand command)
        {
            var result = await _sut.Handle(command, new CancellationToken());

            result.Should().BeTrue();

            await _bus.Received(1).RaiseEvent(Arg.Is<PayLoadCreatedEvent>(p
                => p.EntityId != Guid.Empty &&
                p.CorrelationId == command.CorrelationId &&
                p.Content == command.Content &&
                p.Side == command.Side));
        }

        [Theory, AutoNSubstituteData]
        public async void Handle_WhenPayLoadAlreadyExist_ShouldRaisePayLoadAlreadyCreatedEvent(PayLoadCreateCommand command)
        {
            var result = await _sut.Handle(command, new CancellationToken());

            result.Should().BeTrue();

            await _bus.Received(1).RaiseEvent(Arg.Is<PayLoadCreatedEvent>(p
                => p.EntityId != Guid.Empty &&
                p.CorrelationId == command.CorrelationId &&
                p.Content == command.Content &&
                p.Side == command.Side));
        }
    }
}
