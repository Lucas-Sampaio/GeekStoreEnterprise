using GeekStore.Core.Comunication;
using GeekStore.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekStore.WebApp.MVC.Services
{
    public interface IComprasBffService
    {
        Task<CarrinhoVM> ObterCarrinho();
        Task<int> ObterQuantidadeCarrinho();
        Task<ResponseResult> AdicionarItemCarrinho(ItemCarrinhoViewModel produto);
        Task<ResponseResult> AtualizarItemCarrinho(Guid produtoId, ItemCarrinhoViewModel produto);
        Task<ResponseResult> RemoverItemCarrinho(Guid produtoId);
        Task<ResponseResult> AplicarVoucherCarrinho(string voucher);

        // Pedido
        Task<ResponseResult> FinalizarPedido(PedidoTransacaoVM pedidoTransacao);
        Task<PedidoVM> ObterUltimoPedido();
        Task<IEnumerable<PedidoVM>> ObterListaPorClienteId();
        PedidoTransacaoVM MapearParaPedido(CarrinhoVM carrinho, EnderecoVM endereco);
    }
}
