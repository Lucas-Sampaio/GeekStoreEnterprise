using GeekStore.BFF.Compras.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekStore.BFF.Compras.Services
{
    public interface ICatalogoService
    {
        Task<IEnumerable<ItemProdutoDTO>> ObterTodos();
        Task<ItemProdutoDTO> ObterPorId(Guid id);
        Task<IEnumerable<ItemProdutoDTO>> ObterItens(IEnumerable<Guid> ids);
    }
}