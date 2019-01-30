using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using Waes.Diff.Api.Contracts.Enums;
using Waes.Diff.Api.Controllers;
using Waes.Diff.Api.Factories;
using Waes.Diff.Api.Services;
using Waes.Diff.Api.UnitTests.AutoData;
using Waes.Diff.Core.Factories;
using Waes.Diff.Core.Models;
using Xunit;
using Difference = Waes.Diff.Core.Models.Difference;

namespace Waes.Diff.Api.UnitTests.Services
{
    public class DiffServiceTests
    {
        [Theory, AutoNSubstituteData]
        public void Constructors_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(DiffController).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async void Handle_WhenEqualData_ShouldReturnResponseAsExpected(DiffService sut, string correlationId)
        {
            sut.DiffHandler.Diff(correlationId).Returns(CreateEqualDiffResult());

            var result = await sut.Handle(correlationId);

            result.Success.Should().BeTrue();
            result.Result.Status.Should().Be(DiffStatus.Equal);
            result.Result.Differences.Should().BeNull();

            var dataInfo = result.Result.DataInfo.ToList();

            dataInfo[0].CorrelationId.Should().Be("abc123");
            dataInfo[0].Id.Should().NotBe(Guid.Empty);
            dataInfo[0].Length.Should().Be(3);
            dataInfo[0].Side.Should().Be(Contracts.Enums.SideEnum.Left);

            dataInfo[1].CorrelationId.Should().Be("abc123");
            dataInfo[1].Id.Should().NotBe(Guid.Empty);
            dataInfo[1].Length.Should().Be(3);
            dataInfo[1].Side.Should().Be(Contracts.Enums.SideEnum.Right);
        }

        [Theory, AutoNSubstituteData]
        public async void Handle_WhenNotEqualData_ShouldReturnResponseAsExpected(DiffService sut, string correlationId)
        {
            sut.DiffHandler.Diff(correlationId).Returns(CreateNotEqualDiffResult());

            var result = await sut.Handle(correlationId);

            result.Success.Should().BeTrue();
            result.Result.Status.Should().Be(DiffStatus.NotEqual);
            
            var dataInfo = result.Result.DataInfo.ToList();

            dataInfo[0].CorrelationId.Should().Be("abc123");
            dataInfo[0].Id.Should().NotBe(Guid.Empty);
            dataInfo[0].Length.Should().Be(4);
            dataInfo[0].Side.Should().Be(Contracts.Enums.SideEnum.Left);

            dataInfo[1].CorrelationId.Should().Be("abc123");
            dataInfo[1].Id.Should().NotBe(Guid.Empty);
            dataInfo[1].Length.Should().Be(4);
            dataInfo[1].Side.Should().Be(Contracts.Enums.SideEnum.Right);

            var differences = result.Result.Differences.ToList();

            differences[0].Length.Should().Be(1);
            differences[0].StartOffSet.Should().Be(1);

            differences[1].Length.Should().Be(1);
            differences[1].StartOffSet.Should().Be(3);
        }

        [Theory, AutoNSubstituteData]
        public async void Handle_WhenNotOfEqualSizeData_ShouldReturnResponseAsExpected(DiffService sut, string correlationId)
        {
            sut.DiffHandler.Diff(correlationId).Returns(CreateNotOfEqualSizeDiffResult());

            var result = await sut.Handle(correlationId);

            result.Success.Should().BeTrue();
            result.Result.Status.Should().Be(DiffStatus.NotOfEqualSize);
            result.Result.Differences.Should().BeNull();

            var dataInfo = result.Result.DataInfo.ToList();

            dataInfo[0].CorrelationId.Should().Be("abc123");
            dataInfo[0].Id.Should().NotBe(Guid.Empty);
            dataInfo[0].Length.Should().Be(3);
            dataInfo[0].Side.Should().Be(Contracts.Enums.SideEnum.Left);

            dataInfo[1].CorrelationId.Should().Be("abc123");
            dataInfo[1].Id.Should().NotBe(Guid.Empty);
            dataInfo[1].Length.Should().Be(4);
            dataInfo[1].Side.Should().Be(Contracts.Enums.SideEnum.Right);
        }

        private static DiffResult CreateEqualDiffResult()
        {
            return new DiffResult
            {
                SameSize = true,
                Differences = new List<Difference>(),
                Data = new List<Data>
                {
                    DataFactory.Create(new byte[] { 1, 2, 3}, "abc123", Core.Models.SideEnum.Left),
                    DataFactory.Create(new byte[] { 1, 2, 3}, "abc123", Core.Models.SideEnum.Right)
                }
            };
        }

        private static DiffResult CreateNotEqualDiffResult()
        {
            return new DiffResult
            {
                SameSize = true,
                Differences = new List<Difference>()
                {
                    DifferenceFactory.Create(1, 1),
                    DifferenceFactory.Create(3, 1)
                },
                Data = new List<Data>
                {
                    DataFactory.Create(new byte[] { 1, 7, 3, 4}, "abc123", Core.Models.SideEnum.Left),
                    DataFactory.Create(new byte[] { 1, 2, 3, 8}, "abc123", Core.Models.SideEnum.Right)
                }                
            };
        }

        private static DiffResult CreateNotOfEqualSizeDiffResult()
        {
            return new DiffResult
            {
                SameSize = false,
                Differences = new List<Difference>(),                
                Data = new List<Data>
                {
                    DataFactory.Create(new byte[] { 1, 2, 3}, "abc123", Core.Models.SideEnum.Left),
                    DataFactory.Create(new byte[] { 1, 2, 3, 4}, "abc123", Core.Models.SideEnum.Right)
                }                
            };
        }       
    }
}
