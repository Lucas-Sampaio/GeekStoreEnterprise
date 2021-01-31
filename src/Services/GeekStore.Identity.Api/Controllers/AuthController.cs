using Geek.WebApi.Core.Controller;
using GeekStore.Core.Messages.Integration;
using GeekStore.Identity.Api.Models;
using GeekStore.Identity.Api.Services;
using GeekStore.MessageBus;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GeekStore.Identity.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : MainController
    {
        private readonly AuthenticationService _authenticationService;
        private readonly IMessageBus _bus;
        public AuthController(AuthenticationService authenticationService, IMessageBus bus)
        {
            _authenticationService = authenticationService;
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
            var result = await _authenticationService.UserManager.CreateAsync(user, viewModel.Senha);
            if (result.Succeeded)
            {
                var clienteResult = await RegistrarCliente(viewModel);
                if (!clienteResult.ValidationResult.IsValid)
                {
                    await _authenticationService.UserManager.DeleteAsync(user);
                    return CustomResponse(clienteResult.ValidationResult);
                }
                var token = await _authenticationService.GerarJwt(viewModel.Email);
                return CustomResponse(token);
            }

            foreach (var item in result.Errors)
            {
                AdicionarErroProcessamento(item.Description);
            }
            return CustomResponse();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UsuarioLoginVM usuarioLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _authenticationService.SignInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha,
                false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await _authenticationService.GerarJwt(usuarioLogin.Email));
            }

            if (result.IsLockedOut)
            {
                AdicionarErroProcessamento("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse();
            }

            AdicionarErroProcessamento("Usuário ou Senha incorretos");
            return CustomResponse();
        }

        private async Task<ResponseMessage> RegistrarCliente(UsuarioRegistroVM usuarioRegistro)
        {
            var usuario = await _authenticationService.UserManager.FindByEmailAsync(usuarioRegistro.Email);

            var usuarioRegistrado = new UsuarioRegistradoIntegrationEvent(
                Guid.Parse(usuario.Id), usuarioRegistro.Nome, usuarioRegistro.Email, usuarioRegistro.Cpf);

            try
            {
                return await _bus.RequestAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(usuarioRegistrado);
            }
            catch
            {
                await _authenticationService.UserManager.DeleteAsync(usuario);
                throw;
            }
        }
        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                AdicionarErroProcessamento("Refresh Token inválido");
                return CustomResponse();
            }

            var token = await _authenticationService.ObterRefreshToken(Guid.Parse(refreshToken));

            if (token is null)
            {
                AdicionarErroProcessamento("Refresh Token expirado");
                return CustomResponse();
            }

            return CustomResponse(await _authenticationService.GerarJwt(token.Username));
        }
    }
}
