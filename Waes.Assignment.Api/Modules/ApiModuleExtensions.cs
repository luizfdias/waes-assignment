using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Swagger;
using System.Linq;
using Waes.Assignment.Api.Common;
using Waes.Assignment.Api.Filters;
using Waes.Assignment.Api.Interfaces;

namespace Waes.Assignment.Api.Modules
{
    public static class ApiModuleExtensions
    {
        public static IServiceCollection AddApiModule(this IServiceCollection services)
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Diff API", Version = "v1" });
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IResponseCreator, DiffResponseCreator>();
            services.AddMediatR(typeof(Startup));

            return services;
        }
    }
}
