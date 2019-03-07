using Microsoft.Extensions.DependencyInjection;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Infrastructure.Databases.InMemory;
using Waes.Assignment.Infrastructure.Databases.InMemory.Repositories;

namespace Waes.Assignment.Infrastructure.Modules
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
        {
            services.AddScoped<IRepository<PayLoad>, PayLoadRepository>();
            services.AddSingleton<InMemoryDatabase>();

            return services;
        }
    }
}
