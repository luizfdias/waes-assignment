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
using Waes.Assignment.Api.Handlers;
using Waes.Assignment.Api.Interfaces;
using Waes.Assignment.Infra.IoC;

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

            services.AddMvc(options => options.Filters.Add<ExceptionsFilter>())
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddJsonOptions(options => 
                    {
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    });

            services.AddScoped<IResponseHandler, PayLoadResponseHandler>();
            services.AddMediatR(typeof(Startup));

            DependencyInjector.Initialize(services);
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
