using AutoFixture.Idioms;
using Waes.Diff.Infrastructure.MongoDBStorage.Repositories;
using Waes.Diff.Infrastructure.UnitTests.AutoData;
using Xunit;

namespace Waes.Diff.Infrastructure.UnitTests.MongoStorage.Repositories
{
    public class DataRepositoryTests
    {
        [Theory, AutoNSubstituteData]
        public void Constructors_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(DataRepository).GetConstructors());
        }
    }
}
