using Restaurant.Domain.Abstraction;

namespace Restaurant.Domain.Entities.Identity
{
    public partial class PersonalProfile : SoftDeletableEntity
    {
        public string FirstName { get;  set; } = string.Empty;
        public string LastName { get;  set; } = string.Empty;

        public DateOnly DateOfBirth { get;  set; }
        public bool Gender { get;  set; }

        public string Address { get;  set; } = string.Empty;
        public string City { get;  set; } = string.Empty;
        public string Country { get;  set; } = string.Empty;

        public string Phone { get;  set; } = string.Empty;
        public string CitizenCardId { get;  set; } = string.Empty;

        public int UserId { get;  set; }

        public User User { get;  set; } = null!;
    }

    public partial class PersonalProfile
    {
        public PersonalProfile()
        {

        }

        public PersonalProfile SetUser(int userId)
        {
            UserId = userId;
            return this;
        }
    }
}
