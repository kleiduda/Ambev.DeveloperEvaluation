using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasColumnType("uuid")
                   .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c => c.Date).IsRequired();

            builder.OwnsMany(c => c.Products, pb =>
            {
                pb.ToTable("CartProducts");

                pb.WithOwner().HasForeignKey("CartId");
                pb.Property<Guid>("Id");
                pb.HasKey("Id");

                pb.Property(p => p.ProductId).IsRequired();
                pb.Property(p => p.Quantity).IsRequired();
            });
        }
    }

}
