using System;
using Autofac.Extensions.DependencyInjection;
using MatchList.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace MatchList.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    DatabaseContext.EnsureCreated(services);
                    Log.Information("Database migrated.");
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Database not migrated.");
                }
            }

            host.Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .ConfigureLogging(config => config.ClearProviders())
                .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                                                                     .Enrich.FromLogContext()
                                                                     .Enrich.WithProperty("Application", "MatchList.Api")
                                                                     .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment.EnvironmentName)
                                                                     .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                                                                     .MinimumLevel.Override("System", LogEventLevel.Warning)
                                                                     .WriteTo.Seq(hostingContext.Configuration["Serilog:SeqServerUrl"], LogEventLevel.Information)
                                                                     .WriteTo.Debug()
                                                                     .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"));
    }
}