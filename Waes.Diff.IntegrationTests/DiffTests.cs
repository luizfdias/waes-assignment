using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.IO;
using System.Linq;
using Waes.Diff.Api.Contracts;
using Waes.Diff.Api.Controllers;
using Waes.Diff.IntegrationTests.AutoData;
using Xunit;

namespace Waes.Diff.IntegrationTests
{
    public class DiffTests
    {
        [Theory, AutoNSubstituteData]
        public async void PostLeft_WhenPostLeftIsCalled_ShouldStoreDataAsExpected(DiffController sut, IMemoryCache memory, byte[] data)
        {
            sut.ControllerContext.HttpContext.Request.Body = new MemoryStream(data);

            await sut.PostLeft("Abc123");

            var leftData = memory.Get("left_Abc123");

            leftData.Should().BeOfType<byte[]>();
            leftData.Should().BeEquivalentTo(data);
        }

        [Theory, AutoNSubstituteData]
        public async void PostRight_WhenPostRightIsCalled_ShouldStoreDataAsExpected(DiffController sut, IMemoryCache memory, byte[] data)
        {
            sut.ControllerContext.HttpContext.Request.Body = new MemoryStream(data);

            await sut.PostRight("Abc123");

            var leftData = memory.Get("right_Abc123");

            leftData.Should().BeOfType<byte[]>();
            leftData.Should().BeEquivalentTo(data);
        }


        //// CASE 1: Datas are equals        
        [Theory, AutoNSubstituteData]
        public async void GetDiff_WhenEquals_ShouldReturnDiffResponseAsExpected(DiffController sut, IMemoryCache memory, byte[] data, string id)
        {
            memory.Set($"left_{id}", data);
            memory.Set($"right_{id}", data);

            var result = await sut.GetDiff(id);

            var diffResponse = ((OkObjectResult)result).Value as DiffResponse;

            diffResponse.EqualsSize.Should().BeTrue();
            diffResponse.Differences.Should().BeNull();

            diffResponse.DataInfo.Should().HaveCount(2);
            diffResponse.DataInfo.First().Id.Should().Be($"left_{id}");
            diffResponse.DataInfo.First().Length.Should().Be(data.Length);
        }

        //// CASE 2: Datas have same size but are differents
        [Theory, AutoNSubstituteData]
        public async void GetDiff_WhenSameSizeButDifferents_ShouldReturnDiffResponseAsExpected(DiffController sut, IMemoryCache memory, string id)
        {
            memory.Set($"left_{id}", new byte[] { 1, 2, 3 });
            memory.Set($"right_{id}", new byte[] { 2, 3, 3 });

            var result = await sut.GetDiff(id);

            var diffResponse = ((OkObjectResult)result).Value as DiffResponse;

            diffResponse.EqualsSize.Should().BeTrue();

            diffResponse.Differences.Should().HaveCount(1);
            diffResponse.Differences.FirstOrDefault().StartOffSet.Should().Be(0);
            diffResponse.Differences.FirstOrDefault().Length.Should().Be(2);

            diffResponse.DataInfo.Should().HaveCount(2);
            diffResponse.DataInfo.First().Id.Should().Be($"left_{id}");
            diffResponse.DataInfo.First().Length.Should().Be(3);
        }

        //// CASE 3: Datas don't have same size
        [Theory, AutoNSubstituteData]
        public async void GetDiff_WhenNotSameSize_ShouldReturnDiffResponseAsExpected(DiffController sut, IMemoryCache memory, string id)
        {
            memory.Set($"left_{id}", new byte[] { 1, 2, 3 });
            memory.Set($"right_{id}", new byte[] { 2, 3, 3, 4 });

            var result = await sut.GetDiff(id);

            var diffResponse = ((OkObjectResult)result).Value as DiffResponse;

            diffResponse.EqualsSize.Should().BeFalse();

            diffResponse.Differences.Should().BeNull();

            diffResponse.DataInfo.Should().HaveCount(2);

            diffResponse.DataInfo.ToList()[0].Id.Should().Be($"left_{id}");
            diffResponse.DataInfo.ToList()[0].Length.Should().Be(3);

            diffResponse.DataInfo.ToList()[1].Id.Should().Be($"right_{id}");
            diffResponse.DataInfo.ToList()[1].Length.Should().Be(4);
        }
    }
}
