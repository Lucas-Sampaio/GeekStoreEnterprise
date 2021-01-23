using FluentValidation.Results;
using GeekStore.Core.Data;
using GeekStore.Core.Mediator;
using GeekStore.Core.Messages;
using GeekStore.Pedidos.Domain.Vouchers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Pedidos.Infra
{
    public class PedidosContext : DbContext, IUnitOfWork
    {
        //private readonly IMediatorHandler _mediatorHandler;
        public PedidosContext(DbContextOptions<PedidosContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
           // _mediatorHandler = mediatorHandler;
        }
        public DbSet<Voucher> Vouchers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            var propriedades = modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetProperties().Where(y => y.ClrType == typeof(string)));
            foreach (var property in propriedades)
            {
                property.SetColumnType("varchar(100)");
            }
            var relacoes = modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys());
            foreach (var item in relacoes)
            {
                item.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PedidosContext).Assembly);
        }
        public async Task<bool> Commit()
        {
            var sucesso = await base.SaveChangesAsync() > 0;
            //if (sucesso) await _mediatorHandler.PublicarEventos(this);
            return sucesso;
        }
    }
}
