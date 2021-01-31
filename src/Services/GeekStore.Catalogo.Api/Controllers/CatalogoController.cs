using Geek.WebApi.Core.Controller;
using Geek.WebApi.Core.Identidade;
using GeekStore.Catalogo.Api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekStore.Catalogo.Api.Controllers
{
    [Route("api/[controller]")] 
    public class CatalogoController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;

        public CatalogoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

      
        [HttpGet("produtos")]
        public async Task<PagedResult<Produto>> Index([FromQuery] int pageSize = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            return await _produtoRepository.ObterTodos(pageSize,page,q);
        }

        //[ClaimsAuthorize("Catalogo", "Ler")]
        [HttpGet("produtos/{id}")]
        public async Task<Produto> ProdutoDetalhe(Guid id)
        {
            return await _produtoRepository.ObterPorId(id);
        }
        [HttpGet("produtos/lista/{ids}")]
        public async Task<IEnumerable<Produto>> ObterProdutosPorId(string ids)
        {
            return await _produtoRepository.ObterProdutosPorId(ids);
        }
    }
}
