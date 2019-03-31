using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Waes.Assignment.Api;
using Waes.Assignment.Api.Common;
using Waes.Assignment.Application.ApiModels;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Api
{
    public class DiffResponseCreatorTests
    {
        private readonly DiffResponseCreator _sut;
        
        public DiffResponseCreatorTests()
        {            
            _sut = new DiffResponseCreator();
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
        public void ResponseOK_WhenResultIsNotNull_ShouldReturnSuccessResponseOfIt(EqualResponse value)
        {
            var response = _sut.ResponseOK(value);

            response.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)response;

            objectResult.Value.Should().BeOfType<SuccessResponse<DiffResponse>>();
            ((SuccessResponse<DiffResponse>)objectResult.Value).Data.Should().Be(value);
        }

        [Theory, AutoNSubstituteData]
        public void ResponseCreated_WhenResultIsNotNull_ShouldReturnSuccessResponseAsExpected(CreatePayLoadResponse value)
        {
            var response = _sut.ResponseCreated(value);

            response.Should().BeOfType<CreatedResult>();
            var objectResult = (CreatedResult)response;

            objectResult.Value.Should().BeOfType<SuccessResponse<CreatePayLoadResponse>>();
            ((SuccessResponse<CreatePayLoadResponse>)objectResult.Value).Data.Should().Be(value);
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
