using GeekStore.Core.Comunication;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GeekStore.WebApp.MVC.Controllers
{
    public abstract class MainController : Controller
    {
        protected bool ResponsePossuiErros(ResponseResult response)
        {
            if (response != null && response.Errors.Mensagens.Any())
            {
                foreach (var item in response.Errors.Mensagens)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return true;
            }

            return false;
        }
        protected void AdicionarErroValidacao(string mensagem)
        {
            ModelState.AddModelError(string.Empty, mensagem);
        }

        protected bool OperacaoValida()
        {
            return ModelState.ErrorCount == 0;
        }
    }
}
