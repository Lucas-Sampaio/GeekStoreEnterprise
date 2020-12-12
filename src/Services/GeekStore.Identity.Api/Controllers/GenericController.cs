using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GeekStore.Identity.Api.Controllers
{
    [ApiController]
    public abstract class GenericController : ControllerBase
    {
        protected  ICollection<string> Erros = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(result); 
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                {"Mensagens", Erros.ToArray()}
            }));
        }
        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);

            foreach (var item in erros)
            {
               adicionarErroProcessamento(item.ErrorMessage); 
            }

            return CustomResponse();
        }
        protected bool OperacaoValida()
        {
            return !Erros.Any();
        }

        protected void adicionarErroProcessamento(string erro)
        {
            Erros.Add(erro);
        }

        protected void LimparErrosProcessamneto()
        {
            Erros.Clear();
        }
    }
}
