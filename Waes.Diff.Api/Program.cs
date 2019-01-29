﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using System.Diagnostics.CodeAnalysis;

namespace Waes.Diff.Api
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog();
    }
}
