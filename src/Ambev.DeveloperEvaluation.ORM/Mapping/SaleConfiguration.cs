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

            builder.Property(s => s.SaleNumber).IsRequired().HasMaxLength(50);
            builder.Property(s => s.SaleDate).IsRequired();

            builder.Property(s => s.CustomerId).IsRequired();
            builder.Property(s => s.CustomerName).IsRequired().HasMaxLength(100);

            builder.Property(s => s.BranchId).IsRequired();
            builder.Property(s => s.BranchName).IsRequired().HasMaxLength(100);

            builder.Property(s => s.TotalAmount).HasColumnType("numeric(18,2)");
            builder.Property(s => s.IsCancelled).IsRequired();

            builder.OwnsMany(s => s.Items, i =>
            {
                i.ToTable("SaleItems");

                i.WithOwner().HasForeignKey("SaleId");
                i.Property<Guid>("Id");
                i.HasKey("Id");

                i.Property(p => p.ProductId).IsRequired();
                i.Property(p => p.ProductName).IsRequired().HasMaxLength(100);
                i.Property(p => p.Quantity).IsRequired();
                i.Property(p => p.UnitPrice).HasColumnType("numeric(18,2)").IsRequired();
                i.Property(p => p.Discount).HasColumnType("numeric(18,2)").IsRequired();
            });
        }
    }

}
