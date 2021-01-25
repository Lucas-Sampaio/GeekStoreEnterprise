using Geek.WebApi.Core.Controller;
using Geek.WebApi.Core.Usuario;
using GeekStore.Clientes.Api.Application.Commands;
using GeekStore.Clientes.Api.Models;
using GeekStore.Core.Mediator;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GeekStore.Clientes.Api.Controllers
{
    [Route("api/[controller]")]
    public class ClientesController : MainController
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMediatorHandler _mediator;
        private readonly IAspNetUser _user;

        public ClientesController(IClienteRepository clienteRepository, IMediatorHandler mediator, IAspNetUser user)
        {
            _clienteRepository = clienteRepository;
            _mediator = mediator;
            _user = user;
        }

        [HttpGet("clientes")]
        public async Task<IActionResult> Index()
        {
            var comando = new RegistrarClienteCommand(Guid.NewGuid(), "Lucas", "lucas1997@hotmail.com", "016.025.560-08");

            var resultado = await _mediator.EnviarComando(comando);
            return CustomResponse(resultado);
        }

        [HttpGet("endereco")]
        public async Task<IActionResult> ObterEndereco()
        {
            var endereco = await _clienteRepository.ObterEnderecoPorId(_user.ObterUserId());

            return endereco == null ? NotFound() : CustomResponse(endereco);
        }

        [HttpPost("endereco")]
        public async Task<IActionResult> AdicionarEndereco(AdicionarEnderecoCommand endereco)
        {
            endereco.ClienteId = _user.ObterUserId();
            return CustomResponse(await _mediator.EnviarComando(endereco));
        }
    }
}
