using AutoFixture.Idioms;
using Waes.Assignment.Infrastructure.MongoDBStorage.Repositories;
using Waes.Assignment.Infrastructure.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.Infrastructure.UnitTests.MongoStorage.Repositories
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
