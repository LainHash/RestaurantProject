using Microsoft.AspNetCore.Identity;
using NanoidDotNet;
using System;

namespace Restaurant.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string PublicId { get; set; } = Nanoid.Generate(size: 11);

        public string FullName { get; set; } = string.Empty;

        public string? Avatar { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
