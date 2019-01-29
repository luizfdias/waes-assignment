using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Waes.Diff.Api;
using Waes.Diff.Api.Controllers;
using Waes.Diff.Api.Interfaces;
using Waes.Diff.Core.Interfaces;

namespace Waes.Diff.IntegrationTests.AutoData
{
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute() : base(() =>
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

            var configuration = Substitute.For<IConfiguration>();

            configuration["MemoryStorage:DataExpirationInSeconds"].Returns("60");
            configuration["AppSettings:StorageType"].Returns("Memory");

            fixture.Register(() => configuration);

            var serviceCollection = Substitute.For<ServiceCollection>();

            new Startup(configuration).ConfigureServices(serviceCollection);

            var container = serviceCollection.BuildServiceProvider();

            var diffController = new DiffController(
                container.GetService<IBinaryStorageHandler>(),
                container.GetService<IDiffHandler>(),
                container.GetService<IDiffResponseMapper>());

            diffController.ControllerContext = new ControllerContext();
            diffController.ControllerContext.HttpContext = new DefaultHttpContext();            

            fixture.Register(() => diffController);
            fixture.Register(() => container.GetService<IMemoryCache>());

            return fixture;
        })
        {
        }
    }
}
