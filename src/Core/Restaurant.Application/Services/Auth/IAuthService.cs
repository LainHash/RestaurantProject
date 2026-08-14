using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<Result<AuthResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
        Task<Result<bool>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
        Task<Result<AuthResponse>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken);
        Task<Result<bool>> VerifyEmailAsync(VerifyEmailRequest request, CancellationToken cancellationToken);
    }
}
