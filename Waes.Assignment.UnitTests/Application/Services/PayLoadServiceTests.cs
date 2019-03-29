using AutoFixture.Idioms;
using AutoMapper;
using NSubstitute;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Application.Services;
using Waes.Assignment.Application.ViewModels;
using Waes.Assignment.Domain.Commands;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Application.Services
{
    public class PayLoadServiceTests
    {
        private readonly IMapper _mapper;

        private readonly IMediatorHandler _bus;

        private readonly INotificationHandler _notificationHandler;

        private readonly PayLoadService _sut;

        public PayLoadServiceTests()
        {
            _mapper = Substitute.For<IMapper>();
            _bus = Substitute.For<IMediatorHandler>();
            _notificationHandler = Substitute.For<INotificationHandler>();

            _sut = new PayLoadService(_bus, _mapper, _notificationHandler);
        }

        [Theory, AutoNSubstituteData]
        public void Constructor_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(PayLoadService).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async void Create_GuardTests(string correlationId, CreateLeftPayLoadRequest request, PayLoadCreateCommand payLoadCreateCommand)
        {
            _mapper.Map<PayLoadCreateCommand>(request, opt => opt.Items["correlationId"] = correlationId).Returns(payLoadCreateCommand);

            var result = await _sut.Create(correlationId, request);
        }
    }
}
