using AutoMapper;
using Waes.Assignment.Application.Profiles;

namespace Waes.Assignment.Infra.AutoMapperConfiguration
{
    /// <summary>
    /// AutoMapper configuration helper
    /// </summary>
    public static class AutoMapperConfiguration
    {
        /// <summary>
        /// Creates a new <see cref="IConfigurationProvider"/>
        /// </summary>
        /// <returns></returns>
        public static IConfigurationProvider Create()
        {            
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PayLoadProfile());
                mc.AddProfile(new DiffProfile());
            });

            mappingConfig.AssertConfigurationIsValid();

            return mappingConfig;        
        }
    }
}
