using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.EntityConfigurations
{
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey(e => e.OrderProductId);
            builder.HasOne(e => e.Product).WithMany().HasForeignKey(e => e.ProductId);
            builder.HasOne(e => e.Order).WithMany(o => o.Products).HasForeignKey(e => e.OrderId);
        }
    }
}
