using AutoFixture.Idioms;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute;
using System;
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
        public async void Save_WhenSaveIsCalled_ShouldStoreDataInMemory(string id, byte[] data)
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions());

            var sut = new MemoryRepository(memoryCache, 20);

            await sut.Save(id, data);

            memoryCache.Get<byte[]>(id).Should().BeEquivalentTo(data);
        }

        [Theory, AutoNSubstituteData]
        public async void Get_WhenGetIsCalled_ShouldReturnDataFromMemory(string id, byte[] data)
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions());

            memoryCache.Set(id, data);

            var sut = new MemoryRepository(memoryCache, 1);

            var result = await sut.Get(id);

            result.Should().BeEquivalentTo(data);
        }
    }
}
