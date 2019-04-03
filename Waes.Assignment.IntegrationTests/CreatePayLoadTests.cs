using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Waes.Assignment.Api;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Infra.Interfaces;
using Waes.Assignment.IntegrationTests.Cache;
using Waes.Assignment.IntegrationTests.Database;
using Waes.Assignment.IntegrationTests.Helpers;
using Xunit;

namespace Waes.Assignment.IntegrationTests
{
    public class CreatePayLoadTests 
    {        
        private readonly HttpClient _client;

        private readonly ICache _cache;

        public CreatePayLoadTests()
        {
            _cache = new CacheForTests(new Dictionary<string, object>());
            var payloadDatabase = new InMemoryDatabaseTest<PayLoad>(DatabaseHelper.CreatePayloads());

            var webHost = new WebHostBuilder()
                .ConfigureTestServices(services =>
                {
                    services.AddSingleton(typeof(IDatabase<PayLoad>), payloadDatabase);
                    services.AddScoped(typeof(ICache), ctx => _cache);
                })
                .UseStartup<Startup>();

            var testserver = new TestServer(webHost);

            _client = testserver.CreateClient();
        }

        [Fact]
        public async void PostLeft_WhenCreatingNewPayload_ShouldReturnResultAsExpected()
        {
            var response = await _client.PostAsync("v1/diff/new_payload/left", CreateContent("YWJjMTIz"));

            response.StatusCode.Should().Be(StatusCodes.Status201Created);
        }

        [Fact]
        public async void PostRight_WhenCreatingNewPayload_ShouldReturnResultAsExpected()
        {
            var response = await _client.PostAsync("v1/diff/new_payload/right", CreateContent("YWJjMTIz"));

            response.StatusCode.Should().Be(StatusCodes.Status201Created);
        }

        [Fact]
        public async void PostLeft_WhenPayloadAlreadyExists_ShouldReturnConflict()
        {
            var response = await _client.PostAsync("v1/diff/123456789/left", CreateContent("YWJjMTIz"));

            response.StatusCode.Should().Be(StatusCodes.Status409Conflict);            
        }

        [Fact]
        public async void PostRight_WhenPayloadProcessedIsEqual_DiffCreatedShouldBeEqual()
        {
            var response = await _client.PostAsync("v1/diff/123456789/right", CreateContent("YWJjMTIz"));

            response.StatusCode.Should().Be(StatusCodes.Status201Created);
            var cachedResult = await _cache.GetAsync<EqualDiff>("diff_123456789");
            cachedResult.Should().BeOfType<EqualDiff>();
        }

        [Fact]
        public async void PostRight_WhenPayloadProcessedIsNotEqual_DiffCreatedShouldBeNotEqual()
        {
            var response = await _client.PostAsync("v1/diff/123456789/right", CreateContent("YWJjMzIx"));

            response.StatusCode.Should().Be(StatusCodes.Status201Created);
            var cachedResult = await _cache.GetAsync<NotEqualDiff>("diff_123456789");
            cachedResult.Should().BeOfType<NotEqualDiff>();
        }

        [Fact]
        public async void PostRight_WhenPayloadProcessedIsNotOfEqualSize_DiffCreatedShouldBeNotOfEqualSize()
        {
            var response = await _client.PostAsync("v1/diff/123456789/right", CreateContent("YWJjMzIxMzI="));

            response.StatusCode.Should().Be(StatusCodes.Status201Created);
            var cachedResult = await _cache.GetAsync<NotOfEqualSizeDiff>("diff_123456789");
            cachedResult.Should().BeOfType<NotOfEqualSizeDiff>();
        }

        private static StringContent CreateContent(string content)
        {           
            return new StringContent($"{{ \"data\": {{ \"content\":\"{content}\" }}}}", Encoding.UTF8, "application/json");
        }
    }
}
