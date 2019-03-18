using Microsoft.Extensions.DependencyInjection;
using Waes.Assignment.Infra.IoC.Modules;

namespace Waes.Assignment.Infra.IoC
{
    public class DependencyInjector
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddSerilogModule();
            services.AddApplicationModule();
            services.AddDomainModule();
            services.AddInfraModule();
            services.AddAutoMapperConfiguration();
        }
    }
}
