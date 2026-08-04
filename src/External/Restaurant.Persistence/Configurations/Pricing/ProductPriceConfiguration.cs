using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Pricing;

namespace Restaurant.Persistence.Configurations.Pricing
{
    internal class ProductPriceConfiguration
        : IEntityTypeConfiguration<ProductPrice>
    {
        public void Configure(EntityTypeBuilder<ProductPrice> builder)
        {
            builder.ToTable("ProductPrices");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityByDefaultColumn();

            builder.Property(x => x.PublicId)
                .IsRequired();

            builder.Property(x => x.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(x => x.Product)
                .WithOne(x => x.ProductPrice)
                .HasForeignKey<ProductPrice>(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
