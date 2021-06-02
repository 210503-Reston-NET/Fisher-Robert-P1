using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Serilog;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebUI
{
    public class Program
    {
        public static IConfiguration Config { get; private set; }
        public static void Main(string[] args)
        {

            Config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true,
                    true)
                .AddCommandLine(args)
                .AddEnvironmentVariables()
                .Build();

            // configure serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Config)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .CreateLogger();
            Log.Information("Starting Up");
            CreateHostBuilder(args).Build().Run();
            Log.Information("Stopping");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseConfiguration(Config)
                    .UseSerilog();
                });
    }
}
