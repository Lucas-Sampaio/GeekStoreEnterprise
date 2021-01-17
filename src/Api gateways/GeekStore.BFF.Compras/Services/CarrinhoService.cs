using GeekStore.BFF.Compras.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace GeekStore.BFF.Compras.Services
{
    public class CarrinhoService : ServiceBase, ICarrinhoService
    {
        private readonly HttpClient _httpClient;
        public CarrinhoService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CarrinhoUrl);
        }

    }
}
