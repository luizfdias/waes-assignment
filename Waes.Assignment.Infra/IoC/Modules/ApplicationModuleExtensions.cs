using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Application.NotificationHandlers;
using Waes.Assignment.Application.Services;
using Waes.Assignment.Domain.Events;

namespace Waes.Assignment.Infra.IoC.Modules
{
    public static class ApplicationModuleExtensions
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services.AddScoped<IDiffService, DiffService>();
            services.AddScoped<IPayLoadService, PayLoadService>();

            services.AddScoped<EventListener>();
            services.AddScoped<IListener>(ctx => ctx.GetService<EventListener>());
            services.AddScoped<INotificationHandler<PayLoadCreatedEvent>, DiffListener>();
            services.AddScoped<INotificationHandler<PayLoadCreatedEvent>>(ctx => ctx.GetService<EventListener>());
            services.AddScoped<INotificationHandler<PayLoadAlreadyCreatedEvent>>(ctx => ctx.GetService<EventListener>());
            services.AddScoped<INotificationHandler<DiffNotFoundEvent>>(ctx => ctx.GetService<EventListener>());
            services.AddScoped<INotificationHandler<DiffAnalyzedEvent>>(ctx => ctx.GetService<EventListener>());

            return services;
        }
    }
}
