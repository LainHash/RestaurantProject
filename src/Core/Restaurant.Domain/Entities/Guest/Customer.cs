using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Domain.Entities.Guest
{
    public class Customer : SoftDeletableEntity
    {
        public string CustomerCode { get; private set; } = string.Empty;

        public int UserId { get; private set; }

        public User User { get; private set; } = null!;
    }
}
