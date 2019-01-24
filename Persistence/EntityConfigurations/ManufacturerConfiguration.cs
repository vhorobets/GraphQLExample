using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.EntityConfigurations
{
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.HasKey(e => e.ManufacturerId);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(50).HasDefaultValue("");
            builder.Property(e => e.Country).IsRequired().HasMaxLength(50).HasDefaultValue("");
        }
    }
}