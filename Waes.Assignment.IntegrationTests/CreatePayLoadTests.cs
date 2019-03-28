using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net.Http;
using System.Text;
using Waes.Assignment.Api;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Domain.Models.Enums;
using Waes.Assignment.Infra.Interfaces;
using Waes.Assignment.IntegrationTests.Database;
using Waes.Assignment.IntegrationTests.Helpers;
using Xunit;

namespace Waes.Assignment.IntegrationTests
{
    public class CreatePayLoadTests : IClassFixture<WebApplicationFactory<Startup>>
    {        
        private readonly HttpClient _client;

        private readonly IDatabase<Diff> _diffDatabase;

        public CreatePayLoadTests(WebApplicationFactory<Startup> factory)
        {
            _diffDatabase = new InMemoryDatabaseTest<Diff>();
            var payloadDatabase = new InMemoryDatabaseTest<PayLoad>(DatabaseHelper.CreatePayloads());

            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton(typeof(IDatabase<PayLoad>), payloadDatabase);
                    services.AddSingleton(typeof(IDatabase<Diff>), _diffDatabase);
                });
            }).CreateClient();
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
            _diffDatabase.Entities.FirstOrDefault(x => x.CorrelationId.Equals("123456789")).Info.Status.Should().Be(DiffStatus.Equal);
        }

        [Fact]
        public async void PostRight_WhenPayloadProcessedIsNotEqual_DiffCreatedShouldBeNotEqual()
        {
            var response = await _client.PostAsync("v1/diff/123456789/right", CreateContent("YWJjMzIx"));

            response.StatusCode.Should().Be(StatusCodes.Status201Created);
            _diffDatabase.Entities.FirstOrDefault(x => x.CorrelationId.Equals("123456789")).Info.Status.Should().Be(DiffStatus.NotEqual);
        }

        [Fact]
        public async void PostRight_WhenPayloadProcessedIsNotOfEqualSize_DiffCreatedShouldBeNotOfEqualSize()
        {
            var response = await _client.PostAsync("v1/diff/123456789/right", CreateContent("YWJjMzIxMzI="));

            response.StatusCode.Should().Be(StatusCodes.Status201Created);
            _diffDatabase.Entities.FirstOrDefault(x => x.CorrelationId.Equals("123456789")).Info.Status.Should().Be(DiffStatus.NotOfEqualSize);
        }

        private static StringContent CreateContent(string content)
        {           
            return new StringContent($"{{ \"content\":\"{content}\" }}", Encoding.UTF8, "application/json");
        }
    }
}
