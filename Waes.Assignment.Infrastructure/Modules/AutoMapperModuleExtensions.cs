using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Waes.Assignment.Application.Profiles;

namespace Waes.Assignment.Infrastructure.Modules
{
    public static class AutoMapperModuleExtensions
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {                       
            services.AddScoped<IMapper>(ctx => 
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new PayLoadProfile());
                    mc.AddProfile(new DiffProfile());
                });

                mappingConfig.AssertConfigurationIsValid();

                return mappingConfig.CreateMapper();                
            });
        }
    }
}
