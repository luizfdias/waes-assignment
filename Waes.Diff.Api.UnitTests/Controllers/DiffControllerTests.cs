using AutoFixture.Idioms;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Waes.Diff.Api.Contracts;
using Waes.Diff.Api.Controllers;
using Waes.Diff.Api.UnitTests.AutoData;
using Waes.Diff.Core.Models;
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
        public async void PostLeft_WhenCalled_ShouldCallSaveAsExpected(DiffController sut, string id)
        {
            var result = await sut.PostLeft(id);

            await sut.BinaryStorageHandler.Received(1).Save($"left_{id}", sut.Request.Body);

            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Theory, AutoNSubstituteData]
        public async void PostRight_WhenCalled_ShouldCallSaveAsExpected(DiffController sut, string id)
        {
            var result = await sut.PostRight(id);

            await sut.BinaryStorageHandler.Received(1).Save($"right_{id}", sut.Request.Body);

            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Theory, AutoNSubstituteData]
        public async void GetDiff_WhenCalled_ShouldReturnsDiffResponse(DiffController sut, string id, DiffResult diffResult, DiffResponse diffResponse)
        {
            sut.DiffHandler.Diff(id).Returns(diffResult);

            sut.DiffResponseMapper.Map(diffResult).Returns(diffResponse);

            var result = await sut.GetDiff(id);

            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).Value.Should().BeOfType<DiffResponse>();
        }
    }
}
