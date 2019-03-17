using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Waes.Assignment.Domain.CommandHandlers;
using Waes.Assignment.Domain.Commands;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Services;

namespace Waes.Assignment.Infrastructure.Modules
{
    public static class DomainModuleExtensions
    {
        public static IServiceCollection AddDomainModule(this IServiceCollection services)
        {
            services.AddSingleton<IDiffDomainService, DiffDomainService>();

            services.AddScoped<IRequestHandler<AnalyzeDiffCommand, bool>, DiffCommandHandler>();
            services.AddScoped<IRequestHandler<PayLoadCreateCommand, bool>, PayLoadCommandHandler>();

            return services;
        }
    }
}
