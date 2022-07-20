using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutboxPattern.Services.Ordering.API.Domain.Orders;

namespace OutboxPattern.Services.Ordering.API.Infrastructure.Repositories
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.OwnsMany(x => x.Items, y =>
            {
                y.ToTable("orderItems");

                y.Property(y => y.Quantity).HasColumnName("quantity");
                y.Property(y => y.ProductId).HasColumnName("productId");
            });
        }
    }
}
