using GeekStore.Catalogo.Api.Data;
using GeekStore.Catalogo.Api.Data.Repository;
using GeekStore.Catalogo.Api.Model;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Catalogo.Api.Configuration
{
    public static class DepedencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<CatalogoContext>();
        }
    }
}
