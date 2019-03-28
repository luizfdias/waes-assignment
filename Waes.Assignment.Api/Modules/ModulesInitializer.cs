using Microsoft.Extensions.DependencyInjection;

namespace Waes.Assignment.Api.Modules
{
    public class ModulesInitializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddApiModule();
            services.AddSerilogModule();
            services.AddApplicationModule();
            services.AddDomainModule();
            services.AddInfraModule();
            services.AddAutoMapperConfiguration();
        }
    }
}
