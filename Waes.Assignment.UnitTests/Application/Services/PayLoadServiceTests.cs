using AutoFixture.Idioms;
using AutoMapper;
using NSubstitute;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Application.Services;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Application.Services
{
    public class PayLoadServiceTests
    {
        private readonly IMapper _mapper;

        private readonly IMediatorHandler _mediatorHandler;

        private readonly INotificationHandler _notificationHandler;

        private readonly PayLoadService _payLoadService;

        public PayLoadServiceTests()
        {
            _mapper = Substitute.For<IMapper>();
            _mediatorHandler = Substitute.For<IMediatorHandler>();
            _notificationHandler = Substitute.For<INotificationHandler>();

            _payLoadService = new PayLoadService(_mediatorHandler, _mapper, _notificationHandler);
        }

        [Theory, AutoNSubstituteData]
        public void Constructor_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(PayLoadService).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public void Create_GuardTests(string correlationId)
        {
            //_payLoadService.Create(correlationId, )
        }
    }
}
