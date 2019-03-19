using AutoFixture.Idioms;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Waes.Assignment.Api.Controllers;
using Waes.Assignment.Api.Interfaces;
using Waes.Assignment.Api.ViewModels;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Application.ViewModels;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Api.Controllers
{
    public class DiffControllerTests
    {
        private readonly DiffController _sut;

        private readonly IPayLoadService _payLoadService;

        private readonly IDiffService _diffService;

        private readonly IResponseHandler _responseHandler;

        public DiffControllerTests()
        {
            _payLoadService = Substitute.For<IPayLoadService>();
            _diffService = Substitute.For<IDiffService>();
            _responseHandler = Substitute.For<IResponseHandler>();

            _sut = new DiffController(_payLoadService, _diffService, _responseHandler);
        }

        [Theory, AutoNSubstituteData]
        public void Constructor_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(DiffController).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async void Post_WhenCreateLeftPayLoadRequestIsGiven_ShouldReturnResultAsExpected(string correlationId, CreateLeftPayLoadRequest request, 
            CreatePayLoadResponse response, IActionResult actionResult)
        {
            _payLoadService.Create(correlationId, request).Returns(response);
            _responseHandler.ResponseCreated(_sut, response).Returns(actionResult);

            var result = await _sut.Post(correlationId, request);

            result.Should().Be(actionResult);
        }

        [Theory, AutoNSubstituteData]
        public async void Post_WhenCreateRightPayLoadRequestIsGiven_ShouldReturnResultAsExpected(string correlationId, CreateRightPayLoadRequest request, 
            CreatePayLoadResponse response, IActionResult actionResult)
        {
            _payLoadService.Create(correlationId, request).Returns(response);
            _responseHandler.ResponseCreated(_sut, response).Returns(actionResult);

            var result = await _sut.Post(correlationId, request);

            result.Should().Be(actionResult);
        }

        [Theory, AutoNSubstituteData]
        public async void GetDiff_WhenCorrelationIdIsGiven_ShouldReturnResultAsExpected(string correlationId, DiffResponse response,
            IActionResult actionResult)
        {
            _diffService.Get(correlationId).Returns(response);
            _responseHandler.ResponseOK(_sut, response).Returns(actionResult);

            var result = await _sut.GetDiff(correlationId);

            result.Should().Be(actionResult);
        }
    }
}
