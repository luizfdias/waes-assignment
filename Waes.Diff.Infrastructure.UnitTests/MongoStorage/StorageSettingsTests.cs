using FluentAssertions;
using Waes.Diff.Infrastructure.UnitTests.AutoData;
using Xunit;

namespace Waes.Diff.Infrastructure.UnitTests.MongoStorage
{
    public class StorageSettingsTests
    {
        [Theory]
        [InlineNSubstituteData(true, true, "Container")]
        [InlineNSubstituteData(false, true, "ConnectionString")]
        [InlineNSubstituteData(true, false, "ConnectionString")]
        [InlineNSubstituteData(false, false, "ConnectionString")]
        public void Test(bool isContained, bool isDevelopment, string connectionExpected, StorageSettings storageSettings)
        {
            storageSettings.IsContained = isContained;
            storageSettings.Development = isDevelopment;
            storageSettings.Container = "Container";
            storageSettings.ConnectionString = "ConnectionString";

            storageSettings.ConnectionString.Should().Be(connectionExpected);
        }
    }
}
