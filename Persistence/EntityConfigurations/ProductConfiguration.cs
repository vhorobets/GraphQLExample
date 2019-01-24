using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.ProductId);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100).HasDefaultValue("");
            builder.Property(e => e.Price).IsRequired().HasDefaultValue(0);
            builder.HasOne(e => e.Manufacturer).WithMany(m => m.Products).HasForeignKey(e => e.ManufacturerId);
        }
    }
}
