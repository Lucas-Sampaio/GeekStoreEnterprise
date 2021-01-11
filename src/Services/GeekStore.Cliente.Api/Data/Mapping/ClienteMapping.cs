using GeekStore.Clientes.Api.Models;
using GeekStore.Core.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekStore.Clientes.Api.Data.Mapping
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(c => c.Nome).IsRequired().HasColumnType("varchar(200)");
            builder.OwnsOne(c => c.Cpf, tf =>
             {
                 tf.Property(c => c.Numero)
                 .IsRequired()
                 .HasMaxLength(Cpf.MaxLength)
                 .HasColumnName("Cpf")
                 .HasColumnType($"varchar({Cpf.MaxLength})");
             });
            builder.OwnsOne(c => c.Email, tf =>
            {
                tf.Property(c => c.Endereco)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType($"varchar({Email.MaxLenght})");
            });
            builder.HasOne(c => c.Endereco).WithOne(c => c.Cliente).HasPrincipalKey<Cliente>();
            builder.ToTable("Clientes");
        }
    }
}
