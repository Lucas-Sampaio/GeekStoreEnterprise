using GeekStore.BFF.Compras.Models;
using System.Threading.Tasks;

namespace GeekStore.BFF.Compras.Services
{
    public interface IPedidoService
    {
        Task<VoucherDTO> ObterVoucherPorCodigo(string codigo);
    }

}