using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using AutoMapper;
using Waes.Assignment.Infra.AutoMapperConfiguration;

namespace Waes.Assignment.UnitTests.AutoData
{
    public class AutoNSubstituteDataAutoMapperAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAutoMapperAttribute() : base(() =>
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

            fixture.Register<IMapper>(() => MapperCreator.Create());

            return fixture;
        })
        {
        }
    }
}
