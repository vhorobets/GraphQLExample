using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.EntityConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.OrderId);
            builder.HasOne(e => e.Customer).WithMany().HasForeignKey(e => e.CustomerId);
            builder.Property(e => e.OrderDate).IsRequired().HasDefaultValueSql("GetDate()");
        }
    }
}
