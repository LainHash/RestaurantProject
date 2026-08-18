using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Guest;

namespace Restaurant.Persistence.Configurations.Guest
{
    internal class WalletConfiguration
        : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable("Wallets");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityByDefaultColumn();

            builder.Property(x => x.PublicId)
                .IsRequired();

            builder.Property(x => x.Balance)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasDefaultValue(0m);

            builder.Property(x => x.IsLocked)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasOne(x => x.Customer)
                .WithOne(x => x.Wallet)
                .HasForeignKey<Wallet>(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
