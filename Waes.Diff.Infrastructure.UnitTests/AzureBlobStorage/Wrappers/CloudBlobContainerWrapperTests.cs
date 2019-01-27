using AutoFixture.Idioms;
using Waes.Diff.Infrastructure.AzureBlobStorage.Wrappers;
using Waes.Diff.Infrastructure.UnitTests.AutoData;
using Xunit;

namespace Waes.Diff.Infrastructure.UnitTests.AzureBlobStorage.Wrappers
{
    public class CloudBlobContainerWrapperTests
    {
        [Theory, AutoNSubstituteData]
        public void Constructors_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(CloudBlobContainerWrapper).GetConstructors());
        }
    }
}
