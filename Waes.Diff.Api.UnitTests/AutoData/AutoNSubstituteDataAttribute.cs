using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace Waes.Diff.Api.UnitTests.AutoData
{
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute() : base(() =>
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

            var configuration = Substitute.For<IConfiguration>();
            configuration["BlobStorage:ConnectionString"].Returns("DefaultEndpointsProtocol=https;AccountName=waesdiffstoragefake;AccountKey=saTFakeKey==;EndpointSuffix=core.windows.net");
            configuration["BlobStorage:ContainerName"].Returns("ContainerFake");

            configuration["MemoryStorage:DataExpirationInSeconds"].Returns("60");
            configuration["AppSettings:StorageType"].Returns("Memory");

            fixture.Register(() => configuration);

            return fixture;
        })
        {
        }
    }
}
