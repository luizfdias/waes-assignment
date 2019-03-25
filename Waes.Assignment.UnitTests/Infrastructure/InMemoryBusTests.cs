using AutoFixture.Idioms;
using MediatR;
using NSubstitute;
using Waes.Assignment.Domain.Commands;
using Waes.Assignment.Domain.Events;
using Waes.Assignment.Infra.Bus;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Infrastructure
{
    public class InMemoryBusTests
    {
        private readonly IMediator _mediator;

        private readonly InMemoryBus _bus;

        public InMemoryBusTests()
        {
            _mediator = Substitute.For<IMediator>();

            _bus = new InMemoryBus(_mediator);
        }

        [Theory, AutoNSubstituteData]
        public void Constructor_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(InMemoryBus).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public void RaiseEvent_WhenEventIsRaised_ShouldCallPublish(Event @event)
        {
            _bus.RaiseEvent(@event);

            _mediator.Received(1).Publish(@event);
        }

        [Theory, AutoNSubstituteData]
        public void SendCommand_WhenCommandIsSended_ShouldCallSend(Command command)
        {
            _bus.SendCommand(command);

            _mediator.Received(1).Send(command);
        }
    }
}
