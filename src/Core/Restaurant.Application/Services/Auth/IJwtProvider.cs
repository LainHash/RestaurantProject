using Restaurant.Domain.Entities.Identity;
using System.Security.Claims;

namespace Restaurant.Application.Services.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);

        string GenerateRefreshToken();
        
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
