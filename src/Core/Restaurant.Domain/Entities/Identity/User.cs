using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Guest;

namespace Restaurant.Domain.Entities.Identity
{
    public partial class User : SoftDeletableEntity
    {
        public string UserName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public bool IsActive { get; private set; }

        public int RoleId { get; private set; }

        public Role Role { get; private set; } = null!;
        public ICollection<OtpVerification> OtpVerifications { get; private set; } = [];
        public Customer? Customer { get; private set; } = null!;
        public PersonalProfile? PersonalProfile { get; private set; } = null!;
    }

    public partial class User
    {
        public User() { }

        public User SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
            return this;
        }

        public User SetRole(int roleId)
        {
            RoleId = roleId;
            return this;
        }

        public void CompleteVerification()
        {
            IsActive = true;
        }
    }
}
