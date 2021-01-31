using GeekStore.Core.Messages.Integration;
using GeekStore.Pagamentos.Api.Models;
using System;
using System.Threading.Tasks;

namespace GeekStore.Pagamentos.Api.Services
{
    public interface IPagamentoService
    {
        Task<ResponseMessage> AutorizarPagamento(Pagamento pagamento);
        Task<ResponseMessage> CapturarPagamento(Guid pedidoId);
        Task<ResponseMessage> CancelarPagamento(Guid pedidoId);
    }
}
