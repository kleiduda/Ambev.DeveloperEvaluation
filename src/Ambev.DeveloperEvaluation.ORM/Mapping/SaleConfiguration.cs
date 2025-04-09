using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                   .HasColumnType("uuid")
                   .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(s => s.NumeroVenda).IsRequired().HasMaxLength(50);
            builder.Property(s => s.DataVenda).IsRequired();

            builder.Property(s => s.ClienteId).IsRequired();
            builder.Property(s => s.ClienteNome).IsRequired().HasMaxLength(100);

            builder.Property(s => s.FilialId).IsRequired();
            builder.Property(s => s.FilialNome).IsRequired().HasMaxLength(100);

            builder.Property(s => s.ValorTotal).HasColumnType("numeric(18,2)");
            builder.Property(s => s.Cancelada).IsRequired();

            builder.OwnsMany(s => s.Itens, i =>
            {
                i.ToTable("SaleItems");

                i.WithOwner().HasForeignKey("SaleId");
                i.Property<Guid>("Id");
                i.HasKey("Id");

                i.Property(p => p.ProdutoId).IsRequired();
                i.Property(p => p.ProdutoNome).IsRequired().HasMaxLength(100);
                i.Property(p => p.Quantidade).IsRequired();
                i.Property(p => p.PrecoUnitario).HasColumnType("numeric(18,2)").IsRequired();
                i.Property(p => p.Desconto).HasColumnType("numeric(18,2)").IsRequired();
            });
        }
    }

}
