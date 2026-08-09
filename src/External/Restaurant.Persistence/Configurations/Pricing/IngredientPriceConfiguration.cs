using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Pricing;

namespace Restaurant.Persistence.Configurations.Pricing
{
    internal class IngredientPriceConfiguration
        : IEntityTypeConfiguration<IngredientPrice>
    {
        public void Configure(EntityTypeBuilder<IngredientPrice> builder)
        {
            builder.ToTable("IngredientPrices");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityByDefaultColumn();

            builder.Property(x => x.PublicId)
                .IsRequired();

            builder.Property(x => x.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(x => x.Ingredient)
                .WithOne(x => x.IngredientPrice)
                .HasForeignKey<IngredientPrice>(x => x.IngredientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
