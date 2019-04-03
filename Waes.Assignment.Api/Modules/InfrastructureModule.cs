using Microsoft.Extensions.DependencyInjection;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Infra.Bus;
using Waes.Assignment.Infra.Cache;
using Waes.Assignment.Infra.Interfaces;
using Waes.Assignment.Infra.Repositories;
using Waes.Assignment.Infra.Repositories.Databases.InMemory;

namespace Waes.Assignment.Api.Modules
{
    /// <summary>
    /// Extension of IServiceCollection
    /// </summary>
    public static class InfraModule
    {
        /// <summary>
        /// It adds the Infra dependencies to the container
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfraModule(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IDatabase<>), typeof(InMemoryDatabase<>));

            services.AddScoped<IPayLoadRepository, PayLoadRepository>();            
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            services.AddScoped<ICache, MemoryCacheWrapper>();

            return services;
        }
    }
}
