﻿using GeekStore.Catalogo.Api.Model;
using GeekStore.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Catalogo.Api.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly CatalogoContext _context;
    
        public IUnitOfWork UnitOfWork => _context;

        public ProdutoRepository(CatalogoContext context)
        {
            _context = context;
        }
        public void Adicionar(Produto produto)
        {
            _context.Produtos.Add(produto);
        }

        public void Atualizar(Produto produto)
        {
            _context.Produtos.Update(produto);
        }

        public async Task<Produto> ObterPorId(Guid id)
        {
            return await _context.Produtos.FindAsync(id);;
        }

        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<List<Produto>> ObterProdutosPorId(string ids)
        {
            var idsGuid = ids.Split(',')
              .Select(id => (Ok: Guid.TryParse(id, out var x), Value: x));

            if (!idsGuid.All(nid => nid.Ok)) return new List<Produto>();

            var idsValue = idsGuid.Select(id => id.Value);

            return await _context.Produtos.AsNoTracking()
                .Where(p => idsValue.Contains(p.Id) && p.Ativo).ToListAsync();
        }
    }
}
