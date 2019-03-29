using FluentAssertions;
using NSubstitute;
using System.Collections.Generic;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Domain.Models.Enums;
using Waes.Assignment.Infra.Interfaces;
using Waes.Assignment.Infra.Repositories;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Infrastructure.Repositories
{
    public class PayLoadRepositoryTests
    {
        private readonly IDatabase<PayLoad> _database;

        private readonly PayLoadRepository _sut;

        public PayLoadRepositoryTests()
        {
            _database = Substitute.For<IDatabase<PayLoad>>();

            _sut = new PayLoadRepository(_database);
        }

        [Theory, AutoNSubstituteData]
        public async void GetByCorrelationId_WhenCorrelationIdIsFound_ShouldReturnPayLoadAsExpected(PayLoad item)
        {
            var payLoads = new List<PayLoad> { item, new PayLoad(item.CorrelationId, item.Content, SideEnum.Right) };

            _database.Entities.Returns(payLoads);

            var result = await _sut.GetByCorrelationId(item.CorrelationId);

            result.Should().BeEquivalentTo(payLoads);
        }

        [Theory, AutoNSubstituteData]
        public async void GetByCorrelationId_WhenCorrelationIdIsNotFound_ShouldReturnEmpty(PayLoad item, string anotherCorrelationId)
        {
            var payLoads = new List<PayLoad> { item };

            _database.Entities.Returns(payLoads);

            var result = await _sut.GetByCorrelationId(anotherCorrelationId);

            result.Should().BeEmpty();
        }

        [Theory, AutoNSubstituteData]
        public async void GetByCorrelationIdAndSide_WhenSearchingForLeftPayLoad_ShouldReturnIt(string correlationId,
            byte[] content)
        {
            var leftPayload = new PayLoad(correlationId, content, SideEnum.Left);
            var rightPayload = new PayLoad(correlationId, content, SideEnum.Right);

            var payLoads = new List<PayLoad> { leftPayload, rightPayload };

            _database.Entities.Returns(payLoads);

            var result = await _sut.GetByCorrelationIdAndSide(correlationId, SideEnum.Left);

            result.Should().Be(leftPayload);
        }

        [Theory, AutoNSubstituteData]
        public async void GetByCorrelationIdAndSide_WhenSearchingForRightPayLoad_ShouldReturnIt(string correlationId,
            byte[] content)
        {
            var leftPayload = new PayLoad(correlationId, content, SideEnum.Left);
            var rightPayload = new PayLoad(correlationId, content, SideEnum.Right);

            var payLoads = new List<PayLoad> { leftPayload, rightPayload };

            _database.Entities.Returns(payLoads);

            var result = await _sut.GetByCorrelationIdAndSide(correlationId, SideEnum.Right);

            result.Should().Be(rightPayload);
        }

        [Theory, AutoNSubstituteData]
        public async void GetByCorrelationIdAndSide_WhenPayLoadIsNotFound_ShouldReturnNull(string correlationId, byte[] content)
        {
            var leftPayLoad = new PayLoad(correlationId, content, SideEnum.Left);
            var payLoads = new List<PayLoad> { leftPayLoad };
            
            _database.Entities.Returns(payLoads);

            var result = await _sut.GetByCorrelationIdAndSide(correlationId, SideEnum.Right);

            result.Should().BeNull();
        }
    }
}
