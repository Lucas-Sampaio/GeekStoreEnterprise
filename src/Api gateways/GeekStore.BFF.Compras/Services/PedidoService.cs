using GeekStore.BFF.Compras.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace GeekStore.BFF.Compras.Services
{
    public class PedidoService : ServiceBase, IPedidoService
    {
        private readonly HttpClient _httpClient;

        public PedidoService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.PedidoUrl);
        }
    }
}
