using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Waes.Assignment.Infra.AutoMapperConfiguration;

namespace Waes.Assignment.Api.Modules
{
    /// <summary>
    /// Extension of IServiceCollection
    /// </summary>
    public static class AutoMapperModuleExtensions
    {
        /// <summary>
        /// It adds the AutoMapper dependencies to the container
        /// </summary>
        /// <param name="services"></param>
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {                       
            services.AddScoped<IMapper>(ctx => 
            {
                return AutoMapperConfiguration.Create().CreateMapper();
            });
        }
    }
}
