using Microsoft.Extensions.DependencyInjection;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Infrastructure.CrossCutting.Bus;
using Waes.Assignment.Infrastructure.Repositories.InMemory;

namespace Waes.Assignment.Infrastructure.Modules
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
        {
            services.AddSingleton(typeof(InMemoryDatabase<>));

            services.AddScoped<IPayLoadRepository, PayLoadRepository>();
            services.AddScoped<IDiffRepository, DiffRepository>();
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            return services;
        }
    }
}
