using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Text;
using Waes.Assignment.Api;
using Xunit;

namespace Waes.Assignment.IntegrationTests
{
    public class CreatePayLoadTests : IClassFixture<WebApplicationFactory<Startup>>
    {        
        private readonly HttpClient _client;

        private readonly WebApplicationFactory<Startup> _factory;

        public CreatePayLoadTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async void PostLeft_OnSuccess_ShouldReturnResultAsExpected()
        {
            var response = await _client.PostAsync("v1/diff/abc123/left", CreateContent("YWJjMTIz"));

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<ResponseWrapper>();
            result.Success.Should().BeTrue();
        }

        [Fact]
        public async void PostRight_OnSuccess_ShouldReturnResultAsExpected()
        {
            var response = await _client.PostAsync("v1/diff/abc123/right", CreateContent("YWJjMTIz"));

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<ResponseWrapper>();
            result.Success.Should().BeTrue();
        }

        private static StringContent CreateContent(string content)
        {           
            return new StringContent($"{{ \"content\":\"{content}\" }}", Encoding.UTF8, "application/json");
        }
    }
}
