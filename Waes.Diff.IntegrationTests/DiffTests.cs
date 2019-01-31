using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using Waes.Diff.Api.Contracts;
using Waes.Diff.Api.Contracts.Enums;
using Waes.Diff.Api.Controllers;
using Waes.Diff.Core.Factories;
using Waes.Diff.IntegrationTests.AutoData;
using Xunit;

namespace Waes.Diff.IntegrationTests
{
    public class DiffTests
    {
        [Theory, AutoNSubstituteData]
        public async void PostLeft_WhenPostLeftIsCalled_ShouldStoreDataAsExpected(DiffController sut, string correlationId, BaseRequest<SaveDataModel> request)
        {
            request.Data.Content = new byte[] { 1, 2, 3 };

            var result = await sut.PostLeft(correlationId, request);

            var value = ((CreatedAtActionResult)result).Value;
            var saveDataModel = (BaseResponse<SaveDataModel>)value;

            saveDataModel.Success.Should().BeTrue();
            saveDataModel.Result.Id.Should().NotBe(Guid.Empty);
        }

        [Theory, AutoNSubstituteData]
        public async void PostRight_WhenPostRightIsCalled_ShouldStoreDataAsExpected(DiffController sut, string correlationId, BaseRequest<SaveDataModel> request)
        {
            request.Data.Content = new byte[] { 1, 2, 3 };

            var result = await sut.PostRight(correlationId, request);

            var value = ((CreatedAtActionResult)result).Value;
            var saveDataModel = (BaseResponse<SaveDataModel>)value;

            saveDataModel.Success.Should().BeTrue();
            saveDataModel.Result.Id.Should().NotBe(Guid.Empty);
        }

        [Theory, AutoNSubstituteData]
        public async void GetDiff_WhenEquals_ShouldReturnDiffResponseAsExpected(DiffController sut)
        {            
            var result = await sut.GetDiff("Equals");

            var response = ((OkObjectResult)result).Value as BaseResponse<DiffInfo>;

            response.Result.Status.Should().Be(DiffStatus.Equal);
            response.Result.Differences.Should().BeNull();

            response.Result.DataInfo.Should().HaveCount(2);            
        }

        [Theory, AutoNSubstituteData]
        public async void GetDiff_WhenNotEqual_ShouldReturnDiffResponseAsExpected(DiffController sut)
        {            
            var result = await sut.GetDiff("NotEquals");

            var response = ((OkObjectResult)result).Value as BaseResponse<DiffInfo>;

            response.Result.Status.Should().Be(DiffStatus.NotEqual);
            response.Result.Differences.Should().HaveCount(1);
            response.Result.DataInfo.Should().HaveCount(2);
        }

        [Theory, AutoNSubstituteData]
        public async void GetDiff_WhenNotOfEqualSize_ShouldReturnDiffResponseAsExpected(DiffController sut)
        {            
            var result = await sut.GetDiff("NotOfEqualSize");

            var response = ((OkObjectResult)result).Value as BaseResponse<DiffInfo>;

            response.Result.Status.Should().Be(DiffStatus.NotOfEqualSize);
            response.Result.Differences.Should().BeNull();
            response.Result.DataInfo.Should().HaveCount(2);
        }
    }    
}
