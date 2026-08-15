using Restaurant.Domain.Entities.Identity;

namespace ConvenienceStore.Contract.DTOs.Authentication
{
    public class AuthenticationResponse
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

        public AuthenticationResponse() { }

        public AuthenticationResponse(string userId, string userName, string email, string token)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            Token = token;
        }

        public AuthenticationResponse(User user, string token)
            : this(user.PublicId, user.UserName, user.Email, token)
        {

        }
    }
}
