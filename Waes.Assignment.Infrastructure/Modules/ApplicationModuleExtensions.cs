using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Application.NotificationHandlers;
using Waes.Assignment.Application.Services;
using Waes.Assignment.Domain.Events;

namespace Waes.Assignment.Infrastructure.Modules
{
    public static class ApplicationModuleExtensions
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services.AddScoped<IDiffService, DiffService>();
            services.AddScoped<IPayLoadService, PayLoadService>();

            services.AddScoped<Listener>();

            services.AddScoped<IListener>(ctx => ctx.GetService<Listener>());
            services.AddScoped<INotificationHandler<PayLoadCreatedEvent>, DiffListener>();

            services.AddScoped<INotificationHandler<PayLoadCreatedEvent>>(ctx => ctx.GetService<Listener>());
            services.AddScoped<INotificationHandler<PayLoadAlreadyCreatedEvent>>(ctx => ctx.GetService<Listener>());
            services.AddScoped<INotificationHandler<PayLoadNotFoundEvent>>(ctx => ctx.GetService<Listener>());

            return services;
        }
    }
}
