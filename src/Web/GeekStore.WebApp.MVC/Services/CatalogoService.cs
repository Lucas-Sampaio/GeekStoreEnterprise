using GeekStore.WebApp.MVC.Extensions;
using GeekStore.WebApp.MVC.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeekStore.WebApp.MVC.Services
{
    public class CatalogoService :ServiceBase, ICatalogoService
    {
        private readonly HttpClient _httpClient;

        public CatalogoService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CatalogoUrl);
        }
        public async Task<ProdutoVM> ObterPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/Catalogo/produtos/{id}");
            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<ProdutoVM>(response);
        }

        public async Task<PagedViewModel<ProdutoVM>> ObterTodos(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/api/catalogo/produtos?pageSize={pageSize}&page={pageIndex}&q={query}");
            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<PagedViewModel<ProdutoVM>>(response);
        }
    }
}
