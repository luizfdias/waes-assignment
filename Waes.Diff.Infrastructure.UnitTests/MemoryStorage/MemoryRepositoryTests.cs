using AutoFixture.Idioms;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute;
using System;
using System.Linq;
using Waes.Diff.Core.Models;
using Waes.Diff.Infrastructure.MemoryStorage.Repositories;
using Waes.Diff.Infrastructure.UnitTests.AutoData;
using Xunit;

namespace Waes.Diff.Infrastructure.UnitTests.MemoryStorage
{
    public class MemoryRepositoryTests 
    {
        [Theory, AutoNSubstituteData]
        public void Constructors_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(MemoryRepository).GetConstructors());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Constructors_WhenExpirationInSecondsIsInvalid_ShouldThrowsArgumentException(int expirationInSeconds)
        {
            Assert.Throws<ArgumentException>(() => new MemoryRepository(Substitute.For<IMemoryCache>(), expirationInSeconds));
        }

        [Theory, AutoNSubstituteData]
        public async void Save_WhenSaveIsCalled_ShouldStoreDataInMemory(Data data)
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions());

            var sut = new MemoryRepository(memoryCache, 20);

            await sut.Save(data);

            var dataFromMemory = memoryCache.Get<Data>(data.CorrelationId + data.Side.ToString());

            dataFromMemory.Should().Be(data);
        }

        [Theory, AutoNSubstituteData]
        public async void GetByCorrelationId_WhenGetIsCalled_ShouldReturnDataFromMemory(string id, Data data1, Data data2)
        {
            data1.Side = SideEnum.Left;
            data2.Side = SideEnum.Right;

            var memoryCache = new MemoryCache(new MemoryCacheOptions());

            memoryCache.Set(id + data1.Side.ToString(), data1);
            memoryCache.Set(id + data2.Side.ToString(), data2);

            var sut = new MemoryRepository(memoryCache, 5);

            var result = await sut.GetByCorrelationId(id);

            result.ToList()[0].Should().BeEquivalentTo(data1);
            result.ToList()[1].Should().BeEquivalentTo(data2);
        }
    }
}
