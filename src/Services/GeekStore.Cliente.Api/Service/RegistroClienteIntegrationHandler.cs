using GeekStore.Clientes.Api.Application.Commands;
using GeekStore.Core.Mediator;
using GeekStore.Core.Messages.Integration;
using GeekStore.MessageBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GeekStore.Clientes.Api.Service
{
    public class RegistroClienteIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private IServiceProvider _serviceProvider;
        public RegistroClienteIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }
        private void SetResponder()
        {
            _bus.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(request => RegistrarCliente(request));
            _bus.AdvancedBus.Connected += OnConnect;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            SetResponder();
            return Task.CompletedTask;
        }
        private void OnConnect(object s, EventArgs e)
        {
            SetResponder();
        }
        private ResponseMessage RegistrarCliente(UsuarioRegistradoIntegrationEvent message)
        {
            var clienteCommand = new RegistrarClienteCommand(message.Id, message.Nome, message.Email, message.Cpf);
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            var sucesso = mediator.EnviarComando(clienteCommand).Result;
            return new ResponseMessage(sucesso);
        }
    }
}
