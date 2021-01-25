using GeekStore.Core.Data;
using GeekStore.Pedidos.Domain.Vouchers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekStore.Pedidos.Infra.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly PedidosContext _context;
        public VoucherRepository(PedidosContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Atualizar(Voucher voucher)
        {
            _context.Vouchers.Update(voucher);
        }

        public async Task<Voucher> ObterVoucherPorCodigo(string codigo)
        {
            var voucher = await _context.Vouchers.FirstOrDefaultAsync(x => x.Codigo == codigo);
            return voucher;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
