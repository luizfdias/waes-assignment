using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Waes.Assignment.Api.Handlers;
using Waes.Assignment.Api.Interfaces;

namespace Waes.Assignment.Api.Modules
{
    public static class ApiModuleExtensions
    {
        public static IServiceCollection AddApiModule(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IResponseCreator, DiffResponseCreator>();
            services.AddMediatR(typeof(Startup));

            return services;
        }
    }
}
