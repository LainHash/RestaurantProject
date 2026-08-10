using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Inventory;

namespace Restaurant.Persistence.Configurations.Inventory
{
    internal class IngredientStockConfiguration
        : IEntityTypeConfiguration<IngredientStock>
    {
        public void Configure(EntityTypeBuilder<IngredientStock> builder)
        {
            builder.ToTable("IngredientStocks");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityByDefaultColumn();

            builder.Property(x => x.PublicId)
                .IsRequired();

            builder.Property(x => x.QuantityOnHand)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasIndex(x => new { x.BranchId, x.IngredientId })
                .IsUnique();

            builder.HasOne(x => x.Ingredient)
                .WithMany(x => x.IngredientStocks)
                .HasForeignKey(x => x.IngredientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Branch)
                .WithMany(x => x.IngredientStocks)
                .HasForeignKey(x => x.BranchId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
