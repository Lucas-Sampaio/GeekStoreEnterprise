using GeekStore.BFF.Compras.Models;
using GeekStore.Core.Comunication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekStore.BFF.Compras.Services
{
    public interface IPedidoService
    {
        Task<ResponseResult> FinalizarPedido(PedidoDTO pedido);
        Task<PedidoDTO> ObterUltimoPedido();
        Task<IEnumerable<PedidoDTO>> ObterListaPorClienteId();
        Task<VoucherDTO> ObterVoucherPorCodigo(string codigo);
    }

}