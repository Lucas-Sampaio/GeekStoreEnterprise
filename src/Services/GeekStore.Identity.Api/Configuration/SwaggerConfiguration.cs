using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.AspNetCore.Builder;

namespace GeekStore.Identity.Api.Configuration
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(x => x.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "GeekStore Identity Api",
                Version = "v1",
                Description = "Api de autenticação",
                Contact = new OpenApiContact { Name = "Lucas Sampaio", Email = "lucasssouza29@hotmail.com" },
                License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
            }));

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "My api v1");
            });
            return app;
        }
    }
}
