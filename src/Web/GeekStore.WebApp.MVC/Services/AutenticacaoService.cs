using GeekStore.WebApp.MVC.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace GeekStore.WebApp.MVC.Services
{
    public class AutenticacaoService : ServiceBase, IAutenticacaoService
    {
        private readonly HttpClient _httpClient;

        public AutenticacaoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<UsuarioRespostaLogin> Login(UsuarioLoginVM usuarioLogin)
        {
            var loginContent = new StringContent(JsonSerializer.Serialize(usuarioLogin),Encoding.UTF8,"application/json");
            var response = await _httpClient.PostAsync("https://localhost:5001/api/Auth/Login", loginContent);

            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin()
                {
                    ErroResult =
                        JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }
            
            var result = JsonSerializer.Deserialize<UsuarioRespostaLogin>(await response.Content.ReadAsStringAsync(),options);
            return result;
        }

        public async Task<UsuarioRespostaLogin> Registro(UsuarioRegistroVM usuarioRegistro)
        {
            var registroContent = new StringContent(JsonSerializer.Serialize(usuarioRegistro), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:5001/api/Auth/Registrar", registroContent);
            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin()
                {
                    ErroResult =
                        JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }
            var result = JsonSerializer.Deserialize<UsuarioRespostaLogin>(await response.Content.ReadAsStringAsync(), options);
            return result;
        }
    }
}
