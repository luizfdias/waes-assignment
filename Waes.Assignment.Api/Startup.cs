using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics.CodeAnalysis;
using Waes.Assignment.Api.Filters;
using Waes.Assignment.Application.Notifications;
using Waes.Assignment.Application.Notifications.Interfaces;
using Waes.Assignment.Infrastructure.Modules;

namespace Waes.Assignment.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }        

        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddOptions();

            services.AddSerilogModule();
            services.AddMvc(options => options.Filters.Add<ExceptionsFilter>())
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddJsonOptions(options => 
                    {
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    });

            services.AddApplicationModule();
            services.AddDomainModule();
            services.AddInfrastructureModule();
            services.AddAutoMapperConfiguration();
            services.AddMediatR(typeof(Startup));

            services.AddScoped<WarningNotificationHandler>();
            services.AddScoped<INotificationHandler<WarningNotification>>(ctx => ctx.GetService<WarningNotificationHandler>());
            services.AddScoped<IGetNotifications<WarningNotification>>(ctx => ctx.GetService<WarningNotificationHandler>());
        }

        [ExcludeFromCodeCoverage]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
