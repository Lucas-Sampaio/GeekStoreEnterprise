using GeekStore.Core.Comunication;
using GeekStore.WebApp.MVC.Extensions;
using GeekStore.WebApp.MVC.Models;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeekStore.WebApp.MVC.Services
{
    public class AutenticacaoService : ServiceBase, IAutenticacaoService
    {
        private readonly HttpClient _httpClient;

        public AutenticacaoService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new System.Uri(settings.Value.AutenticacaoUrl);
        }
        public async Task<UsuarioRespostaLogin> Login(UsuarioLoginVM usuarioLogin)
        { 
            var loginContent = ObterConteudo(usuarioLogin);
            var response = await _httpClient.PostAsync("/api/Auth/Login", loginContent);


            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin()
                {
                    ErroResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            var result = await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
            return result;
        }

        public async Task<UsuarioRespostaLogin> Registro(UsuarioRegistroVM usuarioRegistro)
        {
            var registroContent = ObterConteudo(usuarioRegistro);
            var response = await _httpClient.PostAsync("/api/Auth/Registrar", registroContent);

            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin()
                {
                    ErroResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }
            var result = await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
            return result;
        }
    }
}
