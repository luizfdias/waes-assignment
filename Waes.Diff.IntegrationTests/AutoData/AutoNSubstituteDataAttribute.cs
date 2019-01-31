using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
            var hostingEnvironment = Substitute.For<IHostingEnvironment>();

            configuration["MongoDB:ConnectionString"].Returns("mongodb://localhost:27017");
            configuration["MongoDB:Container"].Returns("mongodb://mongo:27017");
            configuration["MongoDB:Database"].Returns("WaesAssignment");

            fixture.Register(() => configuration);

            var serviceCollection = Substitute.For<ServiceCollection>();

            new Startup(configuration, hostingEnvironment).ConfigureServices(serviceCollection);

            serviceCollection.Replace(new ServiceDescriptor(
                typeof(IDataStorage),
                typeof(FakeDataStorage),
                ServiceLifetime.Transient));

            var container = serviceCollection.BuildServiceProvider();

            var diffController = new DiffController(
                container.GetService<IMediator>());

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
