using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;


namespace DynatronChallenge_Back
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
            .AddEnvironmentVariables(prefix: "ASPNETCORE_")
            .Build();
        public static int Main(string[] args)
        {

            try
            {
                Log.Information("Starting web host");

                CreateWebHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseKestrel(o => { o.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(5); })
                .UseIISIntegration()
                .UseIIS()
                .UseConfiguration(Configuration)
                .ConfigureServices(services => services.AddAutofac())
                .ConfigureLogging((context, logging) =>
                {

                    logging.ClearProviders();

                })
                .UseStartup<Startup>();
        }
    }
}
