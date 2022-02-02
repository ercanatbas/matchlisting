using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace MatchList.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddCustomSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                                   {
                                       Version     = "v1",
                                       Title       = "MatchList API",
                                       Description = "This api provides infrastructure for MatchList services",
                                   });
            });
        }

        public static IApplicationBuilder UseCustomSwaggerUi(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocExpansion(DocExpansion.None);
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "MatchList Api v1");
                c.DisplayOperationId();

                c.DefaultModelExpandDepth(2);
                c.DefaultModelRendering(ModelRendering.Model);
                c.DefaultModelsExpandDepth(-1);
                c.DisplayRequestDuration();
                c.EnableDeepLinking();
                c.EnableFilter();
                c.MaxDisplayedTags(5);
                c.ShowExtensions();
                c.EnableValidator();
            });
            return app;
        }
    }
}