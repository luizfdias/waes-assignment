﻿using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using System;
using Waes.Diff.Api.Contracts;
using Waes.Diff.Api.Services;
using Waes.Diff.Api.UnitTests.AutoData;
using Waes.Diff.Core.Models;
using Xunit;

namespace Waes.Diff.Api.UnitTests.Services
{
    public class DataStoreServiceTests
    {
        [Theory, AutoNSubstituteData]
        public void Constructors_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(DataStoreService).GetConstructors());
        }

        [Theory]
        [InlineNSubstituteData(Contracts.Enums.SideEnum.Left, Core.Models.SideEnum.Left)]
        [InlineNSubstituteData(Contracts.Enums.SideEnum.Right, Core.Models.SideEnum.Right)]
        public async void Handle_WhenCallingSaveMethod_ShouldPassTheExpectedParameters(Contracts.Enums.SideEnum side, Core.Models.SideEnum sideExpected, DataStoreService sut, BaseRequest<SaveDataModel> request)
        {
            request.Request.Side = side;

            var result = await sut.Handle(request);

            await sut.DataStorageHandler.Received(1).Save(Arg.Is<Data>(
                x => x.Content == request.Request.Content &&
                x.CorrelationId == request.Request.CorrelationId &&
                x.Id == request.Request.Id &&
                x.Length == request.Request.Content.Length &&
                x.Side == sideExpected));
        }

        [Theory]
        [InlineNSubstituteData(Contracts.Enums.SideEnum.Left, Contracts.Enums.SideEnum.Left)]
        [InlineNSubstituteData(Contracts.Enums.SideEnum.Right, Contracts.Enums.SideEnum.Right)]
        public async void Handle_WhenMethodIsExecuted_ShouldReturnBaseResponseAsExpected(Contracts.Enums.SideEnum side, Contracts.Enums.SideEnum sideExpected, DataStoreService sut, BaseRequest<SaveDataModel> request)
        {
            request.Request.Side = side;

            var result = await sut.Handle(request);

            result.Result.Content.Should().BeEquivalentTo(request.Request.Content);
            result.Result.CorrelationId.Should().Be(request.Request.CorrelationId);
            result.Result.Id.Should().NotBe(Guid.Empty);
            result.Result.Side.Should().Be(sideExpected);
        }
    }
}
