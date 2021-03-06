﻿using AutoFixture.Idioms;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Waes.Assignment.Api.Common;
using Waes.Assignment.Api.Controllers;
using Waes.Assignment.Api.Interfaces;
using Waes.Assignment.Application.ApiModels;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Api.Controllers
{
    public class DiffControllerTests
    {
        private readonly DiffController _sut;

        private readonly IPayLoadService _payLoadService;

        private readonly IDiffService _diffService;

        private readonly IResponseCreator _responseCreator;

        public DiffControllerTests()
        {
            _payLoadService = Substitute.For<IPayLoadService>();
            _diffService = Substitute.For<IDiffService>();
            _responseCreator = Substitute.For<IResponseCreator>();

            _sut = new DiffController(_payLoadService, _diffService, _responseCreator);
        }

        [Theory, AutoNSubstituteData]
        public void Constructor_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(DiffController).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async void PostLeft_OnSuccess_ShouldReturnResponseFromResponseCreated(string correlationId, BaseRequest<CreateLeftPayLoadRequest> request, 
            CreatePayLoadResponse response, IActionResult actionResult)
        {
            _payLoadService.Create(correlationId, request.Data).Returns(response);
            _responseCreator.ResponseCreated(response).Returns(actionResult);

            var result = await _sut.Post(correlationId, request);

            result.Should().Be(actionResult);
        }

        [Theory, AutoNSubstituteData]
        public async void PostLeft_WhenResponseCreatedReturnsNull_ShouldReturnErrorResponse(string correlationId, BaseRequest<CreateLeftPayLoadRequest> request,
            IActionResult errorResponse)
        {
            _payLoadService.Create(correlationId, request.Data).ReturnsNull();
            _responseCreator.ResponseCreated(null).ReturnsNull();
            _responseCreator.ResponseError().Returns(errorResponse);

            var result = await _sut.Post(correlationId, request);

            result.Should().Be(errorResponse);
        }

        [Theory, AutoNSubstituteData]
        public async void PostRight_OnSuccess_ShouldReturnResponseFromResponseCreated(string correlationId, BaseRequest<CreateRightPayLoadRequest> request, 
            CreatePayLoadResponse response, IActionResult actionResult)
        {
            _payLoadService.Create(correlationId, request.Data).Returns(response);
            _responseCreator.ResponseCreated(response).Returns(actionResult);

            var result = await _sut.Post(correlationId, request);

            result.Should().Be(actionResult);
        }

        [Theory, AutoNSubstituteData]
        public async void PostRight_WhenResponseCreatedReturnsNull_ShouldReturnErrorResponse(string correlationId, BaseRequest<CreateRightPayLoadRequest> request,
            IActionResult errorResponse)
        {
            _payLoadService.Create(correlationId, request.Data).ReturnsNull();
            _responseCreator.ResponseCreated(null).ReturnsNull();
            _responseCreator.ResponseError().Returns(errorResponse);

            var result = await _sut.Post(correlationId, request);

            result.Should().Be(errorResponse);
        }

        [Theory, AutoNSubstituteData]
        public async void GetDiff_OnSuccess_ShouldReturnResponseFromResponseOK(string correlationId, DiffResponse response,
            IActionResult actionResult)
        {
            _diffService.Get(correlationId).Returns(response);
            _responseCreator.ResponseOK(response).Returns(actionResult);

            var result = await _sut.GetDiff(correlationId);

            result.Should().Be(actionResult);
        }

        [Theory, AutoNSubstituteData]
        public async void GetDiff_WhenResponseOkReturnsNull_ShouldReturnNotFoundResponse(string correlationId, 
            IActionResult notFoundResponse)
        {
            _diffService.Get(correlationId).ReturnsNull();
            _responseCreator.ResponseOK(null).ReturnsNull();
            _responseCreator.ResponseNotFound().Returns(notFoundResponse);

            var result = await _sut.GetDiff(correlationId);

            result.Should().Be(notFoundResponse);
        }
    }
}
