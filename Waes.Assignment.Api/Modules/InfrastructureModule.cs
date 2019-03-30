using Microsoft.Extensions.DependencyInjection;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Infra.Bus;
using Waes.Assignment.Infra.Interfaces;
using Waes.Assignment.Infra.Repositories;
using Waes.Assignment.Infra.Repositories.Databases.InMemory;

namespace Waes.Assignment.Api.Modules
{
    public static class InfraModule
    {
        public static IServiceCollection AddInfraModule(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IDatabase<>), typeof(InMemoryDatabase<>));

            services.AddScoped<IPayLoadRepository, PayLoadRepository>();
            services.AddScoped<IDiffRepository, DiffRepository>();
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            return services;
        }
    }
}
