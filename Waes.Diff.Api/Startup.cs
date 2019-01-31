using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics.CodeAnalysis;
using Waes.Diff.Api.Contracts;
using Waes.Diff.Api.Filters;
using Waes.Diff.Api.Interfaces;
using Waes.Diff.Api.Modules;
using Waes.Diff.Api.Services;
using Waes.Diff.Core;
using Waes.Diff.Core.Handlers;
using Waes.Diff.Core.Interfaces;
using Waes.Diff.Infrastructure;
using Waes.Diff.Infrastructure.MongoDBStorage;
using Waes.Diff.Infrastructure.MongoDBStorage.Interfaces;
using Waes.Diff.Infrastructure.MongoDBStorage.Repositories;

namespace Waes.Diff.Api
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
            services.Configure<StorageSettings>(
                options =>
                {
                    options.ConnectionString = Configuration["MongoDb:ConnectionString"];
                    options.Database = Configuration["MongoDb:Database"];
                    options.Container = Configuration["MongoDb:Container"];
                    options.IsContained = Configuration["DOTNET_RUNNING_IN_CONTAINER"] != null;
                    options.Development = HostingEnvironment.IsDevelopment();
                });

            services.AddSerilogModule();
            services.AddMvc(options => options.Filters.Add<ExceptionsFilter>())
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddJsonOptions(options => 
                    {
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    });

            //// Api
            services.AddTransient<IMediator, Mediator>();
            services.AddTransient(typeof(IHandleRequest<string, BaseResponse<DiffInfo>>), typeof(DiffService));
            services.AddTransient(typeof(IHandleRequest<BaseRequest<SaveDataModel>, BaseResponse<SaveDataModel>>), typeof(DataStoreService));

            //// Core
            services.AddTransient<IDiffChecker>(x => new NullabilityChecker(new SizeChecker(new BytesChecker())));
            services.AddTransient<IDataStorageHandler, DataStorageHandler>();
            services.AddTransient<IDiffHandler, DiffHandler>();

            //// Infrastructure            
            services.AddTransient<IDataStorage, DataRepository>();
            services.AddTransient<IMongoDBContext, MongoDBContext>();
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
