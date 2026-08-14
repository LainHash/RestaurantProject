using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Persistence.Configurations.Identity
{
    internal class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.Property(r => r.PublicId)
                .IsRequired()
                .HasMaxLength(11);

            builder.HasIndex(r => r.PublicId).IsUnique();
        }
    }
}
