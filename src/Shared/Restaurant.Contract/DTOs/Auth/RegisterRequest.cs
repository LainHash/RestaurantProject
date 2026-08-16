namespace Restaurant.Contract.DTOs.Auth
{
    public class RegisterRequest
    {
        public string UserName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public string ConfirmPassword { get; private set; } = string.Empty;
    }
}
