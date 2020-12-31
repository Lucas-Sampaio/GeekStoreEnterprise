using GeekStore.Catalogo.Api.Model;
using GeekStore.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Catalogo.Api.Data
{
    public class CatalogoContext : DbContext,IUnitOfWork
    {
        public CatalogoContext(DbContextOptions<CatalogoContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var propriedades = modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetProperties().Where(y => y.ClrType == typeof(string)));
            foreach (var property in propriedades)
            {
                property.SetColumnType("varchar(100)");
            }
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);
        }
        public async Task<bool> Commit()
        {
           return await base.SaveChangesAsync() > 0;
        }
    }
}
