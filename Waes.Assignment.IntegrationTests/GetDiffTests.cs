using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Waes.Assignment.Api;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.IntegrationTests.Helpers;
using Xunit;
using Waes.Assignment.Infra.Interfaces;
using Waes.Assignment.IntegrationTests.Database;

namespace Waes.Assignment.IntegrationTests
{
    public class GetDiffTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetDiffTests(WebApplicationFactory<Startup> factory)
        {
            var diffDatabase = new InMemoryDatabaseTest<Diff>(DatabaseHelper.CreateDiffs());

            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton(typeof(IDatabase<Diff>), diffDatabase);
                });
            }).CreateClient();
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
