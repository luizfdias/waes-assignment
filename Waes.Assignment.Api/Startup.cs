using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Waes.Assignment.Api.Common;
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

            services.AddMvc(opt => opt.Filters.Add<ExceptionsFilter>())
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddJsonOptions(opt =>
                    {
                        opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        opt.SerializerSettings.Converters.Add(new StringEnumConverter());
                    })
                    .ConfigureApiBehaviorOptions(opt =>
                    {
                        opt.InvalidModelStateResponseFactory = ctx =>
                        {
                            return new BadRequestObjectResult(new ErrorResponse
                            {
                                Errors = ctx.ModelState.Values.Where(v => v.Errors.Count > 0).SelectMany(entry => entry.Errors).Select(error => new Error(ApiCodes.InvalidRequest, error.ErrorMessage))
                            });
                        };
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
