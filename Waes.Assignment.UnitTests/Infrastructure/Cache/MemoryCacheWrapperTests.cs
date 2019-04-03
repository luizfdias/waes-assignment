using AutoFixture.Idioms;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute;
using System;
using Waes.Assignment.Infra.Cache;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Infrastructure.Cache
{
    public class MemoryCacheWrapperTests
    {
        private readonly IMemoryCache _memoryCache;

        private readonly MemoryCacheWrapper _sut;

        public MemoryCacheWrapperTests()
        {
            _memoryCache = Substitute.For<IMemoryCache>();

            _sut = new MemoryCacheWrapper(_memoryCache);
        }

        [Theory, AutoNSubstituteData]
        public void Constructor_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(MemoryCacheWrapper).GetConstructors());
        }

        [Fact]
        public void Get_WhenCalled_ShouldExecuteCorrectly()
        {
            _memoryCache.TryGetValue(Arg.Any<object>(), out _).Returns(true);
            _sut.GetAsync<string>("key");

            _memoryCache.Received(1).Get("key");
        }

        [Fact]
        public void Get_WhenKeyNotExists_ShouldReturnNull()
        {
            _memoryCache.TryGetValue(Arg.Any<string>(), out _).Returns(false);

            var result = _sut.GetAsync<string>("key").Result;

            result.Should().BeNull();
        }

        [Fact]
        public void Set_WhenCalled_ShouldExecuteCorrectly()
        {
            _sut.SetAsync("key", "value", 1);

            _memoryCache.Received(1).Set("key", "value", TimeSpan.FromSeconds(1));
        }
    }
}
