using Restaurant.Domain.Abstraction;

namespace Restaurant.Domain.Entities.Identity
{
    public class PersonalProfile : SoftDeletableEntity
    {
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;

        public DateOnly DateOfBirth { get; private set; }
        public bool Gender { get; private set; }

        public string Address { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public string Country { get; private set; } = string.Empty;

        public string Phone { get; private set; } = string.Empty;
        public string CitizenCardId { get; private set; } = string.Empty;

        public int UserId { get; private set; }

        public User User { get; private set; } = null!;
    }
}
