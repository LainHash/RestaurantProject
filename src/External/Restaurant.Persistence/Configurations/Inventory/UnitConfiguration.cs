using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Inventory;

namespace Restaurant.Persistence.Configurations.Inventory
{
    internal class UnitConfiguration
        : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.ToTable("Units");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityByDefaultColumn();

            builder.Property(x => x.PublicId)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.Property(x => x.Symbol)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Type)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(x => x.ConversionRate)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasMany(x => x.Products)
                .WithOne(x => x.Unit)
                .HasForeignKey(x => x.UnitId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Ingredients)
                .WithOne(x => x.BaseUnit)
                .HasForeignKey(x => x.BaseUnitId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.RecipeIngredients)
                .WithOne(x => x.Unit)
                .HasForeignKey(x => x.UnitId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
