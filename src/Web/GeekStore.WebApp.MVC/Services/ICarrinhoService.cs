using GeekStore.WebApp.MVC.Models;
using System;
using System.Threading.Tasks;

namespace GeekStore.WebApp.MVC.Services
{
    public interface ICarrinhoService
    {
        Task<CarrinhoVM> ObterCarrinho();
        Task<ResponseResult> AdicionarItemCarrinho(ItemProdutoViewModel produto);
        Task<ResponseResult> AtualizarItemCarrinho(Guid produtoId, ItemProdutoViewModel produto);
        Task<ResponseResult> RemoverItemCarrinho(Guid produtoId);
    }
}
