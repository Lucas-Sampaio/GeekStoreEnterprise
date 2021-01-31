using Geek.WebApi.Core.Usuario;
using GeekStore.Pagamentos.Api.Data;
using GeekStore.Pagamentos.Api.Data.Repository;
using GeekStore.Pagamentos.Api.Facade;
using GeekStore.Pagamentos.Api.Models;
using GeekStore.Pagamentos.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Pagamentos.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddScoped<IPagamentoService, PagamentoService>();
            services.AddScoped<IPagamentoFacade, PagamentoCartaoCreditoFacade>();

            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            services.AddScoped<PagamentoContext>();
        }
    }
}