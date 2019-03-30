using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Waes.Assignment.Api.Modules
{
    /// <summary>
    /// Extension of IServiceCollection
    /// </summary>
    public static class SerilogModuleExtensions
    {
        /// <summary>
        /// It adds the Serilog dependencies to the container
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSerilogModule(this IServiceCollection services)
        {
            services.AddSingleton<ILogger>(x => new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger());

            return services;
        }
    }
}
