using AutoFixture.Idioms;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Linq;
using Waes.Assignment.Api;
using Waes.Assignment.Api.Common;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Domain.Events;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Api
{
    public class DiffResponseCreatorTests
    {
        private readonly DiffResponseCreator _sut;

        private readonly INotificationHandler _notificationHandler;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public DiffResponseCreatorTests()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Scheme = "http";
            httpContext.Request.Host = new HostString("apiteste.com.br");
            httpContext.Request.PathBase = new PathString("");

            _notificationHandler = Substitute.For<INotificationHandler>();
            _httpContextAccessor = Substitute.For<IHttpContextAccessor>();

            _httpContextAccessor.HttpContext.Returns(httpContext);

            _sut = new DiffResponseCreator(_notificationHandler, _httpContextAccessor);
        }

        [Theory, AutoNSubstituteData]
        public void Constructor_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(DiffResponseCreator).GetConstructors());
        }

        [Fact]
        public void ResponseOK_WhenResultIsNull_ShouldReturnNull()
        {
            _sut.ResponseOK(null).Should().BeNull();
        }

        [Fact]
        public void ResponseCreated_WhenResultIsNull_ShouldReturnNull()
        {
            _sut.ResponseCreated(null).Should().BeNull();
        }

        [Theory, AutoNSubstituteData]
        public void ResponseOK_WhenResultIsNotNull_ShouldReturnSuccessResponseOfIt(string value)
        {
            var response = _sut.ResponseOK(value);

            response.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)response;

            objectResult.Value.Should().BeOfType<SuccessResponse>();
            ((SuccessResponse)objectResult.Value).Data.Should().Be(value);
        }

        [Theory, AutoNSubstituteData]
        public void ResponseCreated_WhenResultIsNotNullAndDiffWasNotAnalyzed_ShouldReturnSuccessResponseAsExpected(string value)
        {
            var response = _sut.ResponseCreated(value);

            response.Should().BeOfType<CreatedResult>();
            var objectResult = (CreatedResult)response;

            objectResult.Value.Should().BeOfType<SuccessResponse>();
            ((SuccessResponse)objectResult.Value).Data.Should().Be(value);
            ((SuccessResponse)objectResult.Value).Links.Should().BeNull();
        }

        [Theory, AutoNSubstituteData]
        public void ResponseCreated_WhenResultIsNotNullAndDiffWasAnalyzed_ShouldReturnSuccessResponseAsExpected(string value,
            DiffAnalyzedEvent @event)
        {
            _notificationHandler.GetEvent<DiffAnalyzedEvent>().Returns(@event);

            var response = _sut.ResponseCreated(value);

            response.Should().BeOfType<CreatedResult>();
            var objectResult = (CreatedResult)response;

            objectResult.Value.Should().BeOfType<SuccessResponse>();
            ((SuccessResponse)objectResult.Value).Data.Should().Be(value);
            var link = ((SuccessResponse)objectResult.Value).Links.FirstOrDefault();

            link.Method.Should().Be("GET");
            link.Rel.Should().Be("self");
            link.HRef.Should().Be($"http://apiteste.com.br/v1/diff/{@event.CorrelationId}");
        }

        [Fact]
        public void ResponseError_WhenCalled_ShouldReturnErrorResponse()
        {
            var response = _sut.ResponseError();

            response.Should().BeOfType<ObjectResult>();
            var objectResult = (ObjectResult)response;

            objectResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            objectResult.Value.Should().BeOfType<ErrorResponse>();
            var error = ((ErrorResponse)objectResult.Value).Errors.FirstOrDefault();

            error.Code.Should().Be(ApiCodes.OperationFailure);
            error.Message.Should().Be("An error occurred during the operation.");
        }

        [Fact]
        public void ResponseNotFound_WhenCalled_ShouldReturnErrorResponse()
        {
            var response = _sut.ResponseNotFound();

            response.Should().BeOfType<NotFoundObjectResult>();
            var objectResult = (NotFoundObjectResult)response;

            objectResult.Value.Should().BeOfType<ErrorResponse>();
            var error = ((ErrorResponse)objectResult.Value).Errors.FirstOrDefault();

            error.Code.Should().Be(ApiCodes.EntityNotFound);
            error.Message.Should().Be("Diff result was not found.");
        }
    }
}
