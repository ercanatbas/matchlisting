using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;
using MatchList.Api.Extensions;
using MatchList.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MatchList.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public ILifetimeScope Container     { get; private set; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(env.ContentRootPath)
                          .AddJsonFile("appsettings.json",                        optional: true, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                          .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediator();
            services.AddDatabase(Configuration);
            services.AddContainer();
            services.AddStart();
            services.AddCorses();
            services.AddResponseCompression();
            
            services.AddControllers(options =>
                    {
                        options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                        options.MaxIAsyncEnumerableBufferLimit = 100000;
                    })
                    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.Converters.Add(new StringEnumConverter());
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    });
            services.AddApiVersioning(options =>  
            {  
                options.DefaultApiVersion                   = new ApiVersion(1, 0);  
                options.AssumeDefaultVersionWhenUnspecified = true;  
                options.ReportApiVersions                   = true;  
            }); 
            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat           = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });
            services.AddCustomSwaggerGen();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.AddMediator();
            builder.AddMaps();
            builder.AddServices();
            builder.AddRepositories();
            builder.AddValidators();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCustomSwaggerUi();
                app.UseDeveloperExceptionPage();
            }

            app.UseApiVersioning();
            app.UseResponseCompression();
            app.UseCors(env.EnvironmentName.ToLower());
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            Container = app.ApplicationServices.GetAutofacRoot();
        }
    }
}