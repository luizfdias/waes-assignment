using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using System;
using Waes.Assignment.Api.Interfaces;
using Waes.Assignment.Api.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.Api.UnitTests
{
    public class MediatorTests
    {
        [Theory, AutoNSubstituteData]
        public void GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(Mediator).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async void Send_GuardTest_ShouldThrowArgumentNullException(Mediator sut)
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.Send<string, string>(null));
        }

        [Theory, AutoNSubstituteData]
        public async void Send_WhenServiceIsFound_ShouldCallItAndReturnTheResponse(Mediator sut, IHandleRequest<string, string> handler, string value, string response)
        {
            handler.Handle(value).Returns(response);

            sut.ServiceFactory.GetService(Arg.Any<Type>()).Returns(handler);

            var result = await sut.Send<string, string>(value);

            result.Should().Be(response);
        }
    }
}
