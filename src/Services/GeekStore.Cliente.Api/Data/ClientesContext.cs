using GeekStore.Clientes.Api.Models;
using GeekStore.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Clientes.Api.Data
{
    public class ClientesContext : DbContext, IUnitOfWork
    {
        public ClientesContext(DbContextOptions<ClientesContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientesContext).Assembly);
        }
        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
