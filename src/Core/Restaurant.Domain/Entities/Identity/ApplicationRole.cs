using Microsoft.AspNetCore.Identity;
using NanoidDotNet;
using System;

namespace Restaurant.Domain.Entities.Identity
{
    public class ApplicationRole : IdentityRole<int>
    {
        public string PublicId { get; set; } = Nanoid.Generate(size: 11);

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public ApplicationRole() : base() { }

        public ApplicationRole(string roleName) : base(roleName) { }
    }
}
