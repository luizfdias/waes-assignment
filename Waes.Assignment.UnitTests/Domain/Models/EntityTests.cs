using FluentAssertions;
using System;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Domain.Models
{
    public class EntityTests
    {
        [Theory, AutoNSubstituteData]
        public void Constructor_WhenContructingAnEntity_ShouldSetId(Guid id)
        {
            var entity = new FakeEntity(id);

            entity.Id.Should().Be(id);
        }

        public class FakeEntity : Entity
        {
            public FakeEntity(Guid id) : base(id)
            {
            }
        }
    }
}
