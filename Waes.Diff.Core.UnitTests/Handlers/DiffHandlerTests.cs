using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using Waes.Diff.Core.Exceptions;
using Waes.Diff.Core.Handlers;
using Waes.Diff.Core.Models;
using Waes.Diff.Core.UnitTests.AutoData;
using Xunit;

namespace Waes.Diff.Core.UnitTests.Handlers
{
    public class DiffHandlerTests
    {        
        [Theory, AutoNSubstituteData]
        public void Constructors_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(DiffHandler).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async void Diff_WhenDataFound_ShouldReturnDiffResult(DiffHandler sut, string id, byte[] data1, byte[] data2, DiffResult diffResult)
        {
            sut.DataStorage.Get($"left_{id}").Returns(data1);
            sut.DataStorage.Get($"right_{id}").Returns(data2);

            sut.DiffChecker.Check(data1, data2).Returns(diffResult);

            var result = await sut.Diff(id);

            result.Should().Be(diffResult);

            result.LeftDataInfo.Id.Should().Be($"left_{id}");
            result.LeftDataInfo.Length.Should().Be(data1.Length);

            result.RightDataInfo.Id.Should().Be($"right_{id}");
            result.RightDataInfo.Length.Should().Be(data2.Length);
        }

        [Theory, AutoNSubstituteData]
        public async void Diff_WhenLeftDataNotFound_ShouldThrowDataNotFoundException(DiffHandler sut, string id, byte[] data2)
        {
            sut.DataStorage.Get($"left_{id}").Returns((byte[])null);
            sut.DataStorage.Get($"right_{id}").Returns(data2);
            
            var exception = await Assert.ThrowsAsync<DataNotFoundException>(() => sut.Diff(id));
            exception.Id.Should().Be($"left_{id}");
        }

        [Theory, AutoNSubstituteData]
        public async void Diff_WhenRightDataNotFound_ShouldThrowDataNotFoundException(DiffHandler sut, string id, byte[] data1)
        {
            sut.DataStorage.Get($"left_{id}").Returns(data1);
            sut.DataStorage.Get($"right_{id}").Returns((byte[])null);

            var exception = await Assert.ThrowsAsync<DataNotFoundException>(() => sut.Diff(id));
            exception.Id.Should().Be($"right_{id}");
        }
    }
}