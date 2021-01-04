using Geek.WebApi.Core.Controller;
using GeekStore.Clientes.Api.Application.Commands;
using GeekStore.Core.Mediator;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GeekStore.Clientes.Api.Controllers
{
    [Route("api/[controller]")]
    public class ClienteController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;
        public ClienteController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }
        [HttpGet("clientes")]
        public async Task<IActionResult> Index()
        {
            var comando = new RegistrarClienteCommand(Guid.NewGuid(), "Lucas", "lucas1997@hotmail.com", "016.025.560-08");

            var resultado = await _mediatorHandler.EnviarComando(comando);
            return CustomResponse(resultado);
        }
    }
}
