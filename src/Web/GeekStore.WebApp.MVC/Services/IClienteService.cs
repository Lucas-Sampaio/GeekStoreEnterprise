using GeekStore.Core.Comunication;
using GeekStore.WebApp.MVC.Extensions;
using GeekStore.WebApp.MVC.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeekStore.WebApp.MVC.Services
{
    public interface IClienteService
    {
        Task<EnderecoVM> ObterEndereco();
        Task<ResponseResult> AdicionarEndereco(EnderecoVM endereco);
    }

    public class ClienteService : ServiceBase, IClienteService
    {
        private readonly HttpClient _httpClient;

        public ClienteService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ClienteUrl);
        }

        public async Task<EnderecoVM> ObterEndereco()
        {
            var response = await _httpClient.GetAsync("api/clientes/endereco/");

            if (response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.InternalServerError) return null;

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<EnderecoVM>(response);
        }

        public async Task<ResponseResult> AdicionarEndereco(EnderecoVM endereco)
        {
            var enderecoContent = ObterConteudo(endereco);

            var response = await _httpClient.PostAsync("api/clientes/endereco/", enderecoContent);

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return new ResponseResult();
        }
    }
}
