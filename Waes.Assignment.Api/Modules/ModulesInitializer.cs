using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Waes.Assignment.Api.Modules
{
    public class ModulesInitializer
    {
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
