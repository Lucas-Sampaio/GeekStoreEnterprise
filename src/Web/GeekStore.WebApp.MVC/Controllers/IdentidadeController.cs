using GeekStore.WebApp.MVC.Models;
using GeekStore.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;
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
            await _autenticacaoService.RealizarLogin(resposta);
            return RedirectToAction("Index", "Catalogo");
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

            await _autenticacaoService.RealizarLogin(resposta);

            if (!string.IsNullOrWhiteSpace(returnUrl)) return LocalRedirect(returnUrl);

            return RedirectToAction("Index", "Catalogo");
        }
        [Route("sair")]
        public async Task<IActionResult> Logout()
        {
            await _autenticacaoService.Logout();
            return RedirectToAction("Index", "Catalogo");
        }
    }
}
