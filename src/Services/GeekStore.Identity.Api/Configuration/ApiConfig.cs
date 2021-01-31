using Geek.WebApi.Core.Identidade;
using Geek.WebApi.Core.Usuario;
using GeekStore.Identity.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Security.JwtSigningCredentials.AspNetCore;

namespace GeekStore.Identity.Api.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<AuthenticationService>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseJwksDiscovery();
            return app;
        }
    }
}
