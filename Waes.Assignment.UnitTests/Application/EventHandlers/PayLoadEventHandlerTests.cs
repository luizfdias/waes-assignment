using AutoFixture.Idioms;
using NSubstitute;
using System.Threading;
using Waes.Assignment.Application.EventHandlers;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Domain.Commands;
using Waes.Assignment.Domain.Events;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Application.EventHandlers
{
    public class PayLoadEventHandlerTests
    {
        private readonly IMediatorHandler _bus;

        private readonly PayLoadEventHandler _sut;

        public PayLoadEventHandlerTests()
        {
            _bus = Substitute.For<IMediatorHandler>();

            _sut = new PayLoadEventHandler(_bus);
        }

        [Theory, AutoNSubstituteData]
        public void Constructor_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(PayLoadEventHandler).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public void Handle_WhenPayLoadCreated_ShouldSendAnalyzeDiffCommand(PayLoadCreatedEvent payLoadCreatedEvent)
        {
            _sut.Handle(payLoadCreatedEvent, new CancellationToken());

            _bus.Received(1).SendCommand(Arg.Is<AnalyzeDiffCommand>(a => a.CorrelationId == payLoadCreatedEvent.CorrelationId));
        }
    }
}
