﻿using AutoFixture.Idioms;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Waes.Diff.Infrastructure.MongoDBStorage;
using Waes.Diff.Infrastructure.UnitTests.AutoData;
using Xunit;

namespace Waes.Diff.Infrastructure.UnitTests.MongoStorage
{
    public class MongoDBContextTests
    {
        [Theory, AutoNSubstituteData]
        public void Constructors_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(MongoDBContext).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public void Constructor_WhenConstructorIsCalled_ShouldCreateMongoContext(IOptions<StorageSettings> settings)
        {
            settings.Value.ConnectionString = "mongodb://localhost:27017";
            settings.Value.Container = "mongodb://mongo:27017";
            settings.Value.Database = "WaesAssignment";

            var mongoContext = new MongoDBContext(settings);

            mongoContext.Should().NotBeNull();
        }
    }
}