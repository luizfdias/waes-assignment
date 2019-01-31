using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using Microsoft.Extensions.Options;
using NSubstitute;
using Waes.Diff.Infrastructure;

namespace Waes.Diff.Api.UnitTests.AutoData
{
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute() : base(() =>
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

            var options = Substitute.For<IOptions<StorageSettings>>();
            options.Value.Returns(new StorageSettings
            {
                ConnectionString = "mongodb://localhost:27017",
                Container = "mongodb://mongo:27017",
                Development = true,
                IsContained = false,
                Database = "WaesAssignment"
            });

            fixture.Register(() => options);

            return fixture;
        })
        {
        }
    }
}
