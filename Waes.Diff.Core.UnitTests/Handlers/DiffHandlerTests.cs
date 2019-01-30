using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using System.Collections.Generic;
using Waes.Diff.Core.Factories;
using Waes.Diff.Core.Handlers;
using Waes.Diff.Core.Models;
using Waes.Diff.Core.UnitTests.AutoData;
using Xunit;

namespace Waes.Diff.Core.UnitTests.Handlers
{
    public class DiffHandlerTests
    {        
        [Theory, AutoNSubstituteData]
        public void Constructors_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(DiffHandler).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async void Diff_WhenDataFound_ShouldReturnDiffResult(DiffHandler sut, string correlationId, DiffResult diffResult)
        {
            var data = new List<Data>
            {
                DataFactory.Create(new byte[] { 1, 2, 3}, "abc123", SideEnum.Left),
                DataFactory.Create(new byte[] { 1, 2, 3}, "abc123", SideEnum.Right)
            };

            sut.DataStorage.GetByCorrelationId(correlationId).Returns(data);

            sut.DiffChecker.Check(data[0], data[1]).Returns(diffResult);

            var result = await sut.Diff(correlationId);

            result.Should().Be(diffResult);
            result.Data.Should().BeEquivalentTo(data);
        }        
    }
}