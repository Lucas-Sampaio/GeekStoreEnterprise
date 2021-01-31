using GeekStore.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.WebApp.MVC.Extensions
{
    public class PaginacaoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IPagedList modeloPaginado)
        {
            return View(modeloPaginado);
        }
    }
}
