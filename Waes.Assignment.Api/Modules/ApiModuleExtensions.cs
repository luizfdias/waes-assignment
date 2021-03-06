﻿using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Linq;
using Waes.Assignment.Api.Common;
using Waes.Assignment.Api.Filters;
using Waes.Assignment.Api.Interfaces;

namespace Waes.Assignment.Api.Modules
{
    /// <summary>
    /// Extension of IServiceCollection
    /// </summary>
    public static class ApiModuleExtensions
    {
        /// <summary>
        /// It adds the API dependencies to the container
        /// </summary>
        /// <param name="services"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static IServiceCollection AddApiModule(this IServiceCollection services, IHostingEnvironment env)
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
                    }).AddFluentValidation();

            var basePath = env.ContentRootPath;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Diff API", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(basePath, "Waes.Assignment.Api.xml"));
                c.IncludeXmlComments(Path.Combine(basePath, "Waes.Assignment.Application.xml"));
            });

            services.AddMemoryCache();
            services.AddSingleton<IResponseCreator, DiffResponseCreator>();
            services.AddMediatR(typeof(Startup));

            return services;
        }
    }
}
