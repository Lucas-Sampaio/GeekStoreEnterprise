﻿using GeekStore.Core.Data;
using GeekStore.Pagamentos.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Pagamentos.Api.Data.Repository
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly PagamentoContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public PagamentoRepository(PagamentoContext context)
        {
            _context = context;
        }

        public void AdicionarPagamento(Pagamento pagamento)
        {
            _context.Pagamentos.Add(pagamento);
        }

        public void AdicionarTransacao(Transacao transacao)
        {
            _context.Transacoes.Add(transacao);
        }

        public async Task<Pagamento> ObterPagamentoPorPedidoId(Guid pedidoId)
        {
            return await _context.Pagamentos.AsNoTracking()
                .FirstOrDefaultAsync(p => p.PedidoId == pedidoId);
        }

        public async Task<IEnumerable<Transacao>> ObterTransacaoesPorPedidoId(Guid pedidoId)
        {
            return await _context.Transacoes.AsNoTracking()
               .Where(t => t.Pagamento.PedidoId == pedidoId).ToListAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
