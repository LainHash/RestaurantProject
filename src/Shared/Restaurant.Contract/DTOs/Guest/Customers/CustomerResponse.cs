using Restaurant.Contract.DTOs.Identity.PersonalProfiles;
using Restaurant.Contract.DTOs.Identity.Users;

namespace Restaurant.Contract.DTOs.Guest.Customers
{
    public class CustomerResponse
    {   
        public string Id { get; set; } = string.Empty;
        public string CustomerCode { get; set; } = string.Empty;

        public AccountResponse Account { get; set; } = null!;
        public PersonalProfileResponse PersonalProfile { get; set; } = null!; 
    }
}
