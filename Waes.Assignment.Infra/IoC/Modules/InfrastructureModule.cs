using Microsoft.Extensions.DependencyInjection;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Infra.Bus;
using Waes.Assignment.Infra.Repositories.InMemory;

namespace Waes.Assignment.Infra.IoC.Modules
{
    public static class InfraModule
    {
        public static IServiceCollection AddInfraModule(this IServiceCollection services)
        {
            services.AddSingleton(typeof(InMemoryDatabase<>));

            services.AddScoped<IPayLoadRepository, PayLoadRepository>();
            services.AddScoped<IDiffRepository, DiffRepository>();
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            return services;
        }
    }
}
