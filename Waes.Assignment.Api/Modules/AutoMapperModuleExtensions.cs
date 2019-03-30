using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Waes.Assignment.Infra.AutoMapperConfiguration;

namespace Waes.Assignment.Api.Modules
{
    public static class AutoMapperModuleExtensions
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {                       
            services.AddScoped<IMapper>(ctx => 
            {
                return AutoMapperConfiguration.Create().CreateMapper();
            });
        }
    }
}
