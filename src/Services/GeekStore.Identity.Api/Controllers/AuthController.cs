using Geek.WebApi.Core.Controller;
using Geek.WebApi.Core.Identidade;
using GeekStore.Core.Messages.Integration;
using GeekStore.Identity.Api.Models;
using GeekStore.MessageBus;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace GeekStore.Identity.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TokenConfiguration _tokenConfig;
        private readonly IMessageBus _bus;
        public AuthController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<TokenConfiguration> tokenConfig, IMessageBus bus)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenConfig = tokenConfig.Value;
            _bus = bus;
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> Registrar(UsuarioRegistroVM viewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var user = new IdentityUser
            {
                UserName = viewModel.Email,
                Email = viewModel.Email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, viewModel.Senha);
            if (result.Succeeded)
            {
                var clienteResult = await RegistrarCliente(viewModel);
                if (!clienteResult.ValidationResult.IsValid)
                {
                    await _userManager.DeleteAsync(user);
                    return CustomResponse(clienteResult.ValidationResult);
                }
                var token = await GerarJwt(viewModel.Email);
                return CustomResponse(token);
            }

            foreach (var item in result.Errors)
            {
                AdicionarErroProcessamento(item.Description);
            }
            return CustomResponse();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UsuarioLoginVM viewModel)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                var result = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Senha, false, true);
                if (result.Succeeded)
                {
                    var token = await GerarJwt(viewModel.Email);
                    return CustomResponse(token);
                }
                if (result.IsLockedOut)
                {
                    AdicionarErroProcessamento("Usuário temporariamente bloqueado por tentativas inválidas");
                }
                AdicionarErroProcessamento("Usuário ou senha invalidas");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return CustomResponse();
        }

        private async Task<UsuarioRespostaLogin> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await ObterClaimsUsuario(claims, user);

            var encodedToken = CodificarToken(identityClaims);
            var response = ObterRespostaToken(encodedToken, user, claims);
            return response;
        }

        private UsuarioRespostaLogin ObterRespostaToken(string encodedToken, IdentityUser user, IList<Claim> claims)
        {
            var response = new UsuarioRespostaLogin
            {
                AccessToken = encodedToken,
                ExpireIn = TimeSpan.FromHours(_tokenConfig.ExpiracaoHoras).TotalSeconds,
                UsuarioToken = new UsuarioToken()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(x => new UsuarioClaim { Type = x.Type, Value = x.Value })
                }
            };
            return response;
        }

        private string CodificarToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenConfig.Secret); // o segredo tem tamanho minimo
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfig.Emissor,
                Audience = _tokenConfig.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_tokenConfig.ExpiracaoHoras),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });
            var encodedToken = tokenHandler.WriteToken(token);
            return encodedToken;
        }

        private async Task<ClaimsIdentity> ObterClaimsUsuario(IList<Claim> claims, IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); // id do tojen
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf,
                ToUnixEpochDate(DateTime.UtcNow).ToString())); //quando o token expira
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(),
                ClaimValueTypes.Integer64)); //quando foi emitido

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);
            return identityClaims;
        }

        private static long ToUnixEpochDate(DateTime date) =>
            (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);

        private async Task<ResponseMessage> RegistrarCliente(UsuarioRegistroVM usuarioRegistro)
        {
            var usuario = await _userManager.FindByEmailAsync(usuarioRegistro.Email);
            var usuarioRegistrado = new UsuarioRegistradoIntegrationEvent(Guid.Parse(usuario.Id), usuarioRegistro.Nome, usuarioRegistro.Email, usuarioRegistro.Cpf);

            try
            {
                return await _bus.RequestAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(usuarioRegistrado);
            }
            catch
            {
                await _userManager.DeleteAsync(usuario);
                throw;
            }
        }
    }
}
