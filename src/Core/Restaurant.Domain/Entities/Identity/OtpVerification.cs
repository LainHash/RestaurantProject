using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Enums;

namespace Restaurant.Domain.Entities.Identity
{
    public class OtpVerification : AuditableEntity
    {
        public int UserId { get; private set; }

        public OtpPurpose Purpose { get; private set; }

        public string CodeHash { get; private set; } = null!;

        public DateTime ExpiresAt { get; private set; }
        public DateTime? UsedAt { get; private set; }

        public int FailedAttempts { get; private set; }

        public User User { get; private set; } = null!;
    }
}
