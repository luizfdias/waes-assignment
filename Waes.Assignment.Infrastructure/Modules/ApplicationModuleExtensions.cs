using Microsoft.Extensions.DependencyInjection;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Application.Services;

namespace Waes.Assignment.Infrastructure.Modules
{
    public static class ApplicationModuleExtensions
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services.AddScoped<IDiffAnalyzerService, DiffAnalyzerService>();
            services.AddScoped<IPayLoadCreateService, PayLoadCreateService>();

            return services;
        }
    }
}
