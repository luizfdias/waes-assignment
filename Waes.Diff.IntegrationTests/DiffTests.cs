using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Waes.Diff.Api.Contracts;
using Waes.Diff.Api.Contracts.Enums;
using Waes.Diff.Api.Controllers;
using Waes.Diff.Core.Factories;
using Waes.Diff.Core.Models;
using Waes.Diff.IntegrationTests.AutoData;
using Xunit;

namespace Waes.Diff.IntegrationTests
{
    public class DiffTests
    {
        [Theory, AutoNSubstituteData]
        public async void PostLeft_WhenPostLeftIsCalled_ShouldStoreDataAsExpected(DiffController sut, IMemoryCache memory, string correlationId, BaseRequest<SaveDataModel> request)
        {
            request.Data.Content = new byte[] { 1, 2, 3 };

            await sut.PostLeft(correlationId, request);

            var leftData = memory.Get(correlationId + Api.Contracts.Enums.SideEnum.Left.ToString());

            leftData.Should().BeOfType<Data>();
            ((Data)leftData).Content.Should().BeEquivalentTo(request.Data.Content);
        }

        [Theory, AutoNSubstituteData]
        public async void PostRight_WhenPostRightIsCalled_ShouldStoreDataAsExpected(DiffController sut, IMemoryCache memory, string correlationId, BaseRequest<SaveDataModel> request)
        {
            request.Data.Content = new byte[] { 1, 2, 3 };

            await sut.PostRight(correlationId, request);

            var rightData = memory.Get(correlationId + Api.Contracts.Enums.SideEnum.Right.ToString());

            rightData.Should().BeOfType<Data>();
            ((Data)rightData).Content.Should().BeEquivalentTo(request.Data.Content);
        }

        [Theory, AutoNSubstituteData]
        public async void GetDiff_WhenEquals_ShouldReturnDiffResponseAsExpected(DiffController sut, IMemoryCache memory)
        {
            var dataLeft = DataFactory.Create(new byte[] { 1, 2, 3 }, "abc123", Core.Models.SideEnum.Left);
            var dataRight = DataFactory.Create(new byte[] { 1, 2, 3 }, "abc123", Core.Models.SideEnum.Right);

            memory.Set(dataLeft.CorrelationId + dataLeft.Side.ToString(), dataLeft);
            memory.Set(dataRight.CorrelationId + dataRight.Side.ToString(), dataRight);

            var result = await sut.GetDiff("abc123");

            var response = ((OkObjectResult)result).Value as BaseResponse<DiffInfo>;

            response.Result.Status.Should().Be(DiffStatus.Equal);
            response.Result.Differences.Should().BeNull();

            response.Result.DataInfo.Should().HaveCount(2);            
        }

        [Theory, AutoNSubstituteData]
        public async void GetDiff_WhenNotEqual_ShouldReturnDiffResponseAsExpected(DiffController sut, IMemoryCache memory)
        {
            var dataLeft = DataFactory.Create(new byte[] { 1, 2, 3 }, "abc123", Core.Models.SideEnum.Left);
            var dataRight = DataFactory.Create(new byte[] { 1, 3, 1 }, "abc123", Core.Models.SideEnum.Right);

            memory.Set(dataLeft.CorrelationId + dataLeft.Side.ToString(), dataLeft);
            memory.Set(dataRight.CorrelationId + dataRight.Side.ToString(), dataRight);

            var result = await sut.GetDiff("abc123");

            var response = ((OkObjectResult)result).Value as BaseResponse<DiffInfo>;

            response.Result.Status.Should().Be(DiffStatus.NotEqual);
            response.Result.Differences.Should().HaveCount(1);
            response.Result.DataInfo.Should().HaveCount(2);
        }

        [Theory, AutoNSubstituteData]
        public async void GetDiff_WhenNotOfEqualSize_ShouldReturnDiffResponseAsExpected(DiffController sut, IMemoryCache memory)
        {
            var dataLeft = DataFactory.Create(new byte[] { 1, 2, 3, 4 }, "abc123", Core.Models.SideEnum.Left);
            var dataRight = DataFactory.Create(new byte[] { 1, 3, 1 }, "abc123", Core.Models.SideEnum.Right);

            memory.Set(dataLeft.CorrelationId + dataLeft.Side.ToString(), dataLeft);
            memory.Set(dataRight.CorrelationId + dataRight.Side.ToString(), dataRight);

            var result = await sut.GetDiff("abc123");

            var response = ((OkObjectResult)result).Value as BaseResponse<DiffInfo>;

            response.Result.Status.Should().Be(DiffStatus.NotOfEqualSize);
            response.Result.Differences.Should().BeNull();
            response.Result.DataInfo.Should().HaveCount(2);
        }
    }    
}
