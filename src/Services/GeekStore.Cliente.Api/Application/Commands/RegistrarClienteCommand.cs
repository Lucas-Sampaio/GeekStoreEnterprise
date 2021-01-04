using FluentValidation;
using GeekStore.Core.DomainObjects;
using GeekStore.Core.Messages;
using System;

namespace GeekStore.Clientes.Api.Application.Commands
{
    public class RegistrarClienteCommand : Command
    {
        public RegistrarClienteCommand(Guid id, string nome, string email, string cpf)
        {
            AgregateId = id;
            Id = id;
            Nome = nome;
            Email = email;
            Cpf = cpf;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public override bool isValido()
        {
            ValidationResult = new RegistrarClienteValidation().Validate(this);

            return ValidationResult.IsValid;
        }

    }
    public class RegistrarClienteValidation : AbstractValidator<RegistrarClienteCommand>
    {
        public RegistrarClienteValidation()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O nome do cliente não foi informado");

            RuleFor(x => x.Cpf)
                .Must(TerCpfValido)
                .WithMessage("O cpf informado não é válido.");

            RuleFor(x => x.Email)
                .Must(TerEmailValido)
                .WithMessage("O email informado não é válido.");

        }
        protected static bool TerCpfValido(string cpf)
        {
            return Cpf.Validar(cpf);
        }
        protected static bool TerEmailValido(string email)
        {
            return Email.Validar(email);
        }
    }
}
