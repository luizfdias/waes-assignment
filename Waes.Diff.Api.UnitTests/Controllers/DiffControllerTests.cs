using AutoFixture.Idioms;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Waes.Diff.Api.Contracts;
using Waes.Diff.Api.Controllers;
using Waes.Diff.Api.UnitTests.AutoData;
using Xunit;

namespace Waes.Diff.Api.UnitTests.Controllers
{
    public class DiffControllerTests
    {
        [Theory, AutoNSubstituteData]
        public void Constructors_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(DiffController).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async void PostLeft_WhenCalled_ShouldCallSaveAsExpected(DiffController sut, string correlationId, BaseRequest<SaveDataModel> request, BaseResponse<SaveDataModel> response)
        {
            sut.Mediator.Send<BaseRequest<SaveDataModel>, BaseResponse<SaveDataModel>>(request).Returns(response);

            var result = await sut.PostLeft(correlationId, request);            

            result.Should().BeOfType<CreatedAtActionResult>();
            ((CreatedAtActionResult)result).Value.Should().BeOfType<BaseResponse<SaveDataModel>>();
        }

        [Theory, AutoNSubstituteData]
        public async void PostRight_WhenCalled_ShouldCallSaveAsExpected(DiffController sut, string correlationId, BaseRequest<SaveDataModel> request, BaseResponse<SaveDataModel> response)
        {
            sut.Mediator.Send<BaseRequest<SaveDataModel>, BaseResponse<SaveDataModel>>(request).Returns(response);

            var result = await sut.PostRight(correlationId, request);

            result.Should().BeOfType<CreatedAtActionResult>();
            ((CreatedAtActionResult)result).Value.Should().BeOfType<BaseResponse<SaveDataModel>>();
        }

        [Theory, AutoNSubstituteData]
        public async void GetDiff_WhenCalled_ShouldReturnsDiffResponse(DiffController sut, string correlationId, BaseResponse<DiffInfo> response)
        {
            sut.Mediator.Send<string, BaseResponse<DiffInfo>>(correlationId).Returns(response);

            var result = await sut.GetDiff(correlationId);

            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).Value.Should().BeOfType<BaseResponse<DiffInfo>>();
        }
    }
}
