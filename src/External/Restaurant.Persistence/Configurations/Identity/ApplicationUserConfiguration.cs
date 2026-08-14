using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Persistence.Configurations.Identity
{
    internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.PublicId)
                .IsRequired()
                .HasMaxLength(11);

            builder.HasIndex(u => u.PublicId).IsUnique();

            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Avatar)
                .HasMaxLength(255);
        }
    }
}
