using AutoMapper;
using Waes.Assignment.Application.Profiles;

namespace Waes.Assignment.Infra.AutoMapperConfiguration
{
    public static class MapperCreator
    {
        public static IMapper Create()
        {            
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PayLoadProfile());
                mc.AddProfile(new DiffProfile());
            });

            mappingConfig.AssertConfigurationIsValid();

            return mappingConfig.CreateMapper();        
        }
    }
}
