using AutoFixture.Idioms;
using Waes.Assignment.Application.Validations;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Application.Validations
{
    public class ValidationBehaviorTests
    {
        [Theory, AutoNSubstituteData]
        public void Constructor_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(ValidationBehavior<,>).GetConstructors());
        }
    }
}
