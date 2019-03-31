using AutoFixture.Idioms;
using FluentAssertions;
using System.Threading;
using Waes.Assignment.Application.EventHandlers;
using Waes.Assignment.Domain.Events;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Application.EventHandlers
{
    public class NotificationEventHandlerTests
    {
        private readonly NotificationEventHandler _sut;

        public NotificationEventHandlerTests()
        {
            _sut = new NotificationEventHandler();
        }

        [Theory, AutoNSubstituteData]
        public void Constructor_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(NotificationEventHandler).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public void Handle_OnDiffAnalyzedEvent_ShouldAddItToEventList(DiffAnalyzedEvent @event)
        {
            _sut.Handle(@event, new CancellationToken());

            _sut.GetEvent<DiffAnalyzedEvent>().Should().Be(@event);
        }

        [Theory, AutoNSubstituteData]
        public void Handle_OnPayLoadCreatedEvent_ShouldAddItToEventList(PayLoadCreatedEvent @event)
        {
            _sut.Handle(@event, new CancellationToken());

            _sut.GetEvent<PayLoadCreatedEvent>().Should().Be(@event);
        }

        [Fact]
        public void GetEvent_WhenEventListIsEmpty_ShouldReturnsNull()
        {
            _sut.GetEvent<PayLoadCreatedEvent>().Should().BeNull();
        }
    }
}
