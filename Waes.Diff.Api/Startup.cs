using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using Waes.Diff.Api.Filters;
using Waes.Diff.Api.Modules;
using Waes.Diff.Core;
using Waes.Diff.Core.Handlers;
using Waes.Diff.Core.Interfaces;
using Waes.Diff.Infrastructure.MemoryStorage.Repositories;

namespace Waes.Diff.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }        

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSerilogModule();
            services.AddMvc(options => options.Filters.Add<ExceptionsFilter>())
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddJsonOptions(options => 
                    {
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    });

            //// Api

            //// Core
            services.AddTransient<IDiffChecker>(x => new NullabilityChecker(new SizeChecker(new BytesChecker())));
            services.AddTransient<IDataStorageHandler, DataStorageHandler>();
            services.AddTransient<IDiffHandler, DiffHandler>();

            //// Infrastructure
            //// If the StorageType is AzureBlob, I setup the DataStorage with the BlobStorageRepository. If not, I use the MemoryRepository
            if (Configuration["AppSettings:StorageType"].Equals("AzureBlob", StringComparison.InvariantCultureIgnoreCase))
            {
                
            }
            else
            {
                services.AddTransient<IDataStorage>(x => new MemoryRepository(x.GetService<IMemoryCache>(), Convert.ToInt32(Configuration["MemoryStorage:DataExpirationInSeconds"])));
            }

            services.AddMemoryCache();            
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
