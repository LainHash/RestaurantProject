namespace Restaurant.Contract.DTOs.Identity.Users
{
    public class AccountResponse
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
}
