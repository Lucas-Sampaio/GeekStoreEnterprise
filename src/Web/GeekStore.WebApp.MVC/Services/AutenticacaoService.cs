using Geek.WebApi.Core.Usuario;
using GeekStore.Core.Comunication;
using GeekStore.WebApp.MVC.Extensions;
using GeekStore.WebApp.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GeekStore.WebApp.MVC.Services
{
    public class AutenticacaoService : ServiceBase, IAutenticacaoService
    {
        private readonly HttpClient _httpClient;

        private readonly IAspNetUser _user;
        private readonly IAuthenticationService _authenticationService;

        public AutenticacaoService(HttpClient httpClient, IOptions<AppSettings> settings, IAspNetUser user, IAuthenticationService authenticationService)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new System.Uri(settings.Value.AutenticacaoUrl);
            _user = user;
            _authenticationService = authenticationService;
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

        public async Task Logout()
        {
            await _authenticationService.SignOutAsync(
                            _user.ObterHttpContext(),
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            null);
        }

        public async Task RealizarLogin(UsuarioRespostaLogin resposta)
        {
            var token = ObterTokenFormatado(resposta.AccessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", resposta.AccessToken));
            claims.Add(new Claim("RefreshToken", resposta.RefreshToken));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8),
                IsPersistent = true
            };

            await _authenticationService.SignInAsync(
                _user.ObterHttpContext(),
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public async Task<bool> RefreshTokenValido()
        {
            var resposta = await UtilizarRefreshToken(_user.ObterUserRefreshToken());

            if (resposta.AccessToken != null && resposta.ErroResult == null)
            {
                await RealizarLogin(resposta);
                return true;
            }

            return false; throw new System.NotImplementedException();
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
        public static JwtSecurityToken ObterTokenFormatado(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
        }
        public async Task<UsuarioRespostaLogin> UtilizarRefreshToken(string refreshToken)
        {
            var refreshTokenContent = ObterConteudo(refreshToken);

            var response = await _httpClient.PostAsync("/api/Auth/refresh-token", refreshTokenContent);

            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin
                {
                    ErroResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
        }
        public bool TokenExpirado()
        {
            var jwt = _user.ObterUserToken();
            if (jwt is null) return false;

            var token = ObterTokenFormatado(jwt);
            return token.ValidTo.ToLocalTime() < DateTime.Now;
        }
    }
}
