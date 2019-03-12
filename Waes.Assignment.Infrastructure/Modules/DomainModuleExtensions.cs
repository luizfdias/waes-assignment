using Microsoft.Extensions.DependencyInjection;
using System;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Services;

namespace Waes.Assignment.Infrastructure.Modules
{
    public static class DomainModuleExtensions
    {
        public static IServiceCollection AddDomainModule(this IServiceCollection services)
        {
            services.AddSingleton<IDiffDomainService<Byte>, DiffDomainService>();

            return services;
        }
    }
}
