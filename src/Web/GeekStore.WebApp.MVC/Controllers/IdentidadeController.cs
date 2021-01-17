using GeekStore.WebApp.MVC.Models;
using GeekStore.WebApp.MVC.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GeekStore.WebApp.MVC.Controllers
{
    public class IdentidadeController : MainController
    {
        private readonly IAutenticacaoService _autenticacaoService;
        public IdentidadeController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }
        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registro(UsuarioRegistroVM model)
        {
            if (!ModelState.IsValid) return View(model);
            var resposta = await _autenticacaoService.Registro(model);
            if (ResponsePossuiErros(resposta.ErroResult)) return View(model);
            await RealizarLogin(resposta);
            return View();
        }
      
        [Route("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UsuarioLoginVM model, string returnUrl = null)
        {
            if (!ModelState.IsValid) return View(model);

            var resposta = await _autenticacaoService.Login(model);

            if (ResponsePossuiErros(resposta.ErroResult)) return View(model);

            await RealizarLogin(resposta);

            if (!string.IsNullOrWhiteSpace(returnUrl)) return LocalRedirect(returnUrl);

            return View();
        }
        [Route("sair")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Catalogo");
        }

        private async Task RealizarLogin(UsuarioRespostaLogin resposta)
        {
            var token = ObterTokenFormatado(resposta.AccessToken);
            var claims = new List<Claim>
            {
                new Claim("JWT",resposta.AccessToken)
            };
            claims.AddRange(token.Claims);
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60), IsPersistent = true };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        private static JwtSecurityToken ObterTokenFormatado(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
        }
    }
}
