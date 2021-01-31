using FluentValidation.Results;
using GeekStore.Core.Data;
using System.Threading.Tasks;

namespace GeekStore.Core.Messages
{
    public class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }
        protected void AdicionarErro(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }
        protected async Task<ValidationResult> PersistirDados(IUnitOfWork uow)
        {
            if (!await uow.Commit())
            {
                AdicionarErro("Houve um erro ao persistir dado");
            }
            return ValidationResult;
        }
    }
}
