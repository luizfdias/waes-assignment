using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace Waes.Assignment.UnitTests.AutoData
{
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute() : base(() => new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }
    }
}
