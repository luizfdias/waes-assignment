using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Waes.Assignment.Application.CommandHandlers;
using Waes.Assignment.Domain.Commands;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Api.Modules
{
    public static class DomainModuleExtensions
    {
        public static IServiceCollection AddDomainModule(this IServiceCollection services)
        {
            services.AddSingleton<IDiffEngine, DiffEngine>();

            services.AddScoped<IRequestHandler<AnalyzeDiffCommand, bool>, DiffCommandHandler>();
            services.AddScoped<IRequestHandler<PayLoadCreateCommand, bool>, PayLoadCommandHandler>();

            return services;
        }
    }
}
