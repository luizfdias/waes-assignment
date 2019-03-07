﻿using AutoFixture.Xunit2;
using Xunit;

namespace Waes.Assignment.Infrastructure.UnitTests.AutoData
{
    public class InlineNSubstituteDataAttribute : CompositeDataAttribute
    {
        public InlineNSubstituteDataAttribute(params object[] values)
            : base(new InlineDataAttribute(values), new AutoNSubstituteDataAttribute())
        {
        }
    }
}
