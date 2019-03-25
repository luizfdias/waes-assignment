using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Waes.Assignment.Application.EventHandlers;
using Waes.Assignment.Application.Interfaces;
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

            services.AddScoped<NotificationEventHandler>();
            services.AddScoped<INotificationHandler>(ctx => ctx.GetService<NotificationEventHandler>());
            services.AddScoped<INotificationHandler<PayLoadCreatedEvent>, PayLoadEventHandler>();
            services.AddScoped<INotificationHandler<PayLoadCreatedEvent>>(ctx => ctx.GetService<NotificationEventHandler>());
            services.AddScoped<INotificationHandler<DiffAnalyzedEvent>>(ctx => ctx.GetService<NotificationEventHandler>());

            return services;
        }
    }
}
