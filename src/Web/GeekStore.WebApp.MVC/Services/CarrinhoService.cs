using GeekStore.WebApp.MVC.Extensions;
using GeekStore.WebApp.MVC.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeekStore.WebApp.MVC.Services
{
    public class CarrinhoService : ServiceBase, ICarrinhoService
    {
        private readonly HttpClient _httpClient;

        public CarrinhoService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CarrinhoUrl);
        }

        public async Task<CarrinhoVM> ObterCarrinho()
        {
            var response = await _httpClient.GetAsync("api/carrinho/");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<CarrinhoVM>(response);
        }

        public async Task<ResponseResult> AdicionarItemCarrinho(ItemProdutoViewModel produto)
        {
            var itemContent = ObterConteudo(produto);

            var response = await _httpClient.PostAsync("api/carrinho/", itemContent);

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return new ResponseResult();
        }

        public async Task<ResponseResult> AtualizarItemCarrinho(Guid produtoId, ItemProdutoViewModel produto)
        {
            var itemContent = ObterConteudo(produto);

            var response = await _httpClient.PutAsync($"api/carrinho/{produto.ProdutoId}", itemContent);

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return new ResponseResult();
        }

        public async Task<ResponseResult> RemoverItemCarrinho(Guid produtoId)
        {
            var response = await _httpClient.DeleteAsync($"api/carrinho/{produtoId}");

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return new ResponseResult();
        }
    }
}
