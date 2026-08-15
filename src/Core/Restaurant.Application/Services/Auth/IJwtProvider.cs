namespace Restaurant.Application.Services.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(string userId, string userName, string email, string role);
    }
}
