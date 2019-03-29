using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using System.Collections.Generic;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Infra.Interfaces;
using Waes.Assignment.Infra.Repositories;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Infrastructure.Repositories
{
    public class DiffRepositoryTests
    {
        private readonly IDatabase<Diff> _database;

        private readonly DiffRepository _sut;

        public DiffRepositoryTests()
        {
            _database = Substitute.For<IDatabase<Diff>>();

            _sut = new DiffRepository(_database);
        }

        [Theory, AutoNSubstituteData]
        public void Constructor_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(DiffRepository).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async void GetByCorrelationId_WhenCorrelationIdIsFound_ShouldReturnDiffAsExpected(EqualDiff item)
        {
            _database.Entities.Returns(new List<Diff> { item });

            var result = await _sut.GetByCorrelationId(item.CorrelationId);

            result.Should().Be(item);
        }

        [Theory, AutoNSubstituteData]
        public async void GetByCorrelationId_WhenCorrelationIdIsNotFound_ShouldReturnNull(EqualDiff item, string anotherCorrelationId)
        {
            _database.Entities.Returns(new List<Diff> { item });

            var result = await _sut.GetByCorrelationId(anotherCorrelationId);

            result.Should().BeNull();
        }
    }
}
