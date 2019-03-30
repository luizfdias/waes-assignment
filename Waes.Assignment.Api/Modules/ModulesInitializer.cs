using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Waes.Assignment.Api.Modules
{
    /// <summary>
    /// It manages the container setup
    /// </summary>
    public static class ModulesInitializer
    {
        /// <summary>
        /// It initializes all modules of the container
        /// </summary>
        /// <param name="services"></param>
        /// <param name="env"></param>
        public static void Initialize(IServiceCollection services, IHostingEnvironment env)
        {
            services.AddApiModule(env);
            services.AddSerilogModule();
            services.AddApplicationModule();
            services.AddDomainModule();
            services.AddInfraModule();
            services.AddAutoMapperConfiguration();
        }
    }
}
