using FluentValidation.Results;
using GeekStore.Clientes.Api.Application.Events;
using GeekStore.Clientes.Api.Models;
using GeekStore.Core.Messages;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GeekStore.Clientes.Api.Application.Commands
{
    public class ClienteCommandHandler : CommandHandler, IRequestHandler<RegistrarClienteCommand, ValidationResult>
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<ValidationResult> Handle(RegistrarClienteCommand request, CancellationToken cancellationToken)
        {
            if (!request.isValido()) return request.ValidationResult;

            var cliente = new Cliente(request.Id, request.Nome, request.Email, request.Cpf);

            var clienteExistente = await _clienteRepository.ObterPorCpf(cliente.Cpf.Numero);

            if (clienteExistente != null)
            {
                AdicionarErro("Este cpf já está em uso.");
                return ValidationResult;
            }
            _clienteRepository.Adicionar(cliente);
            cliente.AdicionarEvento(new ClienteRegistradoEvent(request.Id, request.Nome, request.Email, request.Cpf));
            return await PersistirDados(_clienteRepository.UnitOfWork);
        }
    }
}
