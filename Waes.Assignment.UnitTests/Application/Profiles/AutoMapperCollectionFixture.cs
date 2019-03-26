using AutoMapper;
using Waes.Assignment.Application.Profiles;
using Xunit;

namespace Waes.Assignment.UnitTests.Application.Profiles
{
    [CollectionDefinition("Automapper collection")]
    public class AutoMapperCollectionFixture : ICollectionFixture<AutoMapperFixture>
    {
    }

    public class AutoMapperFixture
    {
        public AutoMapperFixture()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DiffProfile>();
                cfg.AddProfile<PayLoadProfile>();
            });
        }
    }
}
