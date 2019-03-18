using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Waes.Assignment.Infra.IoC.Modules
{
    public static class SerilogModuleExtensions
    {
        public static IServiceCollection AddSerilogModule(this IServiceCollection services)
        {
            services.AddSingleton<ILogger>(x => new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger());

            return services;
        }
    }
}
