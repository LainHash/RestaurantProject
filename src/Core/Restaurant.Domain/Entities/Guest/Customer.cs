using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Entities.Storage;

namespace Restaurant.Domain.Entities.Guest
{
    public partial class Customer : SoftDeletableEntity
    {
        public long CustomerNumber { get; private set; }

        public string CustomerCode =>
            $"CUS-{CustomerNumber:D6}";

        public int UserId { get; private set; }

        public int? AvatarImageId { get; private set; }

        public Image? AvatarImage { get; private set; } = null!;

        public User User { get; private set; } = null!;

        public Wallet? Wallet { get; private set; }
    }

    public partial class Customer
    {
        public Customer() { }

        public Customer(int userId)
        {
            UserId = userId;
        }
    }
}
