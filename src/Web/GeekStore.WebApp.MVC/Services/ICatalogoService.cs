using GeekStore.WebApp.MVC.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekStore.WebApp.MVC.Services
{
    public interface ICatalogoService
    {
        Task<PagedViewModel<ProdutoVM>> ObterTodos(int pageSize, int pageIndex, string query = null);
        Task<ProdutoVM> ObterPorId(Guid id);
    }
    public interface ICatalogoServiceRefit
    {
        [Get("/api/Catalogo/produtos")]
        Task<IEnumerable<ProdutoVM>> ObterTodos();
        [Get("/api/Catalogo/produtos/{id}")]
        Task<ProdutoVM> ObterPorId(Guid id);
    }
}
