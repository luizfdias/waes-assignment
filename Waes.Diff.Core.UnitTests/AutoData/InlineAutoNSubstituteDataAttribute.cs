using AutoFixture.Xunit2;
using Xunit;

namespace Waes.Diff.Core.UnitTests.AutoData
{
    public class InlineNSubstituteDataAttribute : CompositeDataAttribute
    {
        public InlineNSubstituteDataAttribute(params object[] values)
            : base(new InlineDataAttribute(values), new AutoNSubstituteDataAttribute())
        {
        }
    }
}
