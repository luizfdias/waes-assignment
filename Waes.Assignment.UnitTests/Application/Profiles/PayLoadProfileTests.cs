using AutoMapper;
using FluentAssertions;
using Waes.Assignment.Api.ViewModels;
using Waes.Assignment.Application.ViewModels;
using Waes.Assignment.Domain.Commands;
using Waes.Assignment.Domain.Events;
using Waes.Assignment.Domain.Models.Enums;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Application.Profiles
{
    public class PayLoadProfileTests
    {
        [Theory, AutoNSubstituteDataAutoMapper]
        public void Map_WhenMappingFromCreateLeftPayLoadRequestToPayLoadCreateCommand_ShouldMapAsExpected(IMapper sut, 
            CreateLeftPayLoadRequest createLeftPayLoadRequest, string correlationId)
        {
            var result = sut.Map<PayLoadCreateCommand>(createLeftPayLoadRequest, ctx => ctx.Items["correlationId"] = correlationId);

            result.CorrelationId.Should().Be(correlationId);
            result.Content.Should().BeEquivalentTo(createLeftPayLoadRequest.Content);
            result.Side.Should().Be(SideEnum.Left);
        }

        [Theory, AutoNSubstituteDataAutoMapper]
        public void Map_WhenMappingFromCreateRightPayLoadRequestToPayLoadCreateCommand_ShouldMapAsExpected(IMapper sut,
            CreateRightPayLoadRequest createRightPayLoadRequest, string correlationId)
        {
            var result = sut.Map<PayLoadCreateCommand>(createRightPayLoadRequest, ctx => ctx.Items["correlationId"] = correlationId);

            result.CorrelationId.Should().Be(correlationId);
            result.Content.Should().BeEquivalentTo(createRightPayLoadRequest.Content);
            result.Side.Should().Be(SideEnum.Right);
        }

        [Theory, AutoNSubstituteDataAutoMapper]
        public void Map_WhenMappingFromPayLoadCreatedEventToCreatePayLoadResponse_ShouldMapAsExpected(IMapper sut,
            PayLoadCreatedEvent payLoadCreatedEvent)
        {
            var result = sut.Map<CreatePayLoadResponse>(payLoadCreatedEvent);

            result.Content.Should().BeEquivalentTo(payLoadCreatedEvent.Content);
            result.CorrelationId.Should().Be(result.CorrelationId);
            result.Id.Should().Be(result.Id);
        }
    }
}
