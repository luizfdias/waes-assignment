using System.Net.Http;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Waes.Assignment.Api;
using Waes.Assignment.IntegrationTests.Helpers;
using Xunit;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.IntegrationTests.Cache;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Waes.Assignment.IntegrationTests
{
    public class GetDiffTests 
    {
        private readonly HttpClient _client;

        public GetDiffTests()
        {            
            var webHost = new WebHostBuilder()
                .ConfigureTestServices(services =>
                {
                    services.AddScoped(typeof(ICache), ctx => new CacheForTests(CacheHelper.CreateDiff()));
                })
                .UseStartup<Startup>();

            var testserver = new TestServer(webHost);

            _client = testserver.CreateClient();
        }

        [Fact]
        public async void GetDiff_WhenDiffIsFound_ShouldReturnOK()
        {
            var response = await _client.GetAsync("v1/diff/789456123");

            response.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async void GetDiff_WhenDiffIsNotFound_ShouldReturnNotFound()
        {
            var response = await _client.GetAsync("v1/diff/abc123");

            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
