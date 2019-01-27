using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Waes.Diff.Core;
using Waes.Diff.Core.Handlers;
using Waes.Diff.Core.Interfaces;
using Waes.Diff.Infrastructure.AzureBlobStorage.Factories;
using Waes.Diff.Infrastructure.AzureBlobStorage.Interfaces;
using Waes.Diff.Infrastructure.AzureBlobStorage.Repositories;
using Waes.Diff.Infrastructure.AzureBlobStorage.Wrappers;

namespace Waes.Diff.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<IDiffChecker>(x => new NullabilityChecker(new SizeChecker(new BytesChecker())));

            services.AddTransient<IBinaryStorageHandler, BinaryStorageHandler>();
            services.AddTransient<IDiffHandler, DiffHandler>();

            services.AddTransient<IBlobStorageFactory>(x => new BlobStorageFactory(Configuration["BlobStorage:ConnectionString"]));
            services.AddTransient<ICloudBlobContainerWrapper>(x => new CloudBlobContainerWrapper(x.GetService<IBlobStorageFactory>(), Configuration["BlobStorage:ContainerName"]));
            services.AddTransient<IBinaryDataStorage, BlobStorageRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
