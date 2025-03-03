﻿using GeekStore.WebApp.MVC.Models;
using GeekStore.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.WebApp.MVC.Controllers
{
    public class ClienteController : MainController
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> NovoEndereco(EnderecoVM endereco)
        {
            var response = await _clienteService.AdicionarEndereco(endereco);

            if (ResponsePossuiErros(response)) TempData["Erros"] =
                ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();

            return RedirectToAction("EnderecoEntrega", "Pedido");
        }
    }
}
