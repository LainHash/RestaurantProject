using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Domain.Entities.Guest
{
    public partial class Customer : SoftDeletableEntity
    {
        public long CustomerNumber { get; private set; }

        public string CustomerCode =>
            $"CUS-{CustomerNumber:D6}";

        public int UserId { get; private set; }

        public User User { get; private set; } = null!;
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
