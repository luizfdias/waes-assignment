using AutoFixture.Idioms;
using Waes.Diff.Infrastructure.AzureBlobStorage.Factories;
using Waes.Diff.Infrastructure.UnitTests.AutoData;
using Xunit;

namespace Waes.Diff.Infrastructure.UnitTests.AzureBlobStorage.Factories
{
    public class BlobStorageFactoryTests
    {
        [Theory, AutoNSubstituteData]
        public void Constructors_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(BlobStorageFactory).GetConstructors());
        }
    }
}
