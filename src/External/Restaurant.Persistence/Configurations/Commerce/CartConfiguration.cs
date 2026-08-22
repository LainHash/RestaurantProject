using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Commerce;

namespace Restaurant.Persistence.Configurations.Commerce
{
    internal class CartConfiguration
        : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityByDefaultColumn();

            builder.Property(x => x.PublicId)
                .IsRequired();

            builder.Property(x => x.CustomerId);

            builder.Property(x => x.SessionId)
                .HasMaxLength(128);

            builder.HasOne(x => x.Customer)
                .WithOne(x => x.Cart)
                .HasForeignKey<Cart>(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Cart_Owner",
                    @"(
                    (""CustomerId"" IS NOT NULL AND ""SessionId"" IS NULL)
                    OR
                    (""CustomerId"" IS NULL AND ""SessionId"" IS NOT NULL)
                )");
            });

            builder.HasIndex(x => x.CustomerId)
                .IsUnique()
                .HasFilter("\"CustomerId\" IS NOT NULL");

            builder.HasIndex(x => x.SessionId)
                .IsUnique()
                .HasFilter("\"SessionId\" IS NOT NULL");
        }
    }
}
