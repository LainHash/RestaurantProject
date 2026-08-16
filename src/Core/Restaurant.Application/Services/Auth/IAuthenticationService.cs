using ConvenienceStore.Contract.DTOs.Authentication;
using Restaurant.Application.Features.Auth.Commands.Register;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Auth
{
    public interface IAuthenticationService
    {
        Task<Result<AuthenticationResponse>> LoginAsync(
            LoginRequest request,
            CancellationToken cancellationToken = default);

        Task<Result> RegisterAsync(
            RegisterCommand command,
            CancellationToken cancellationToken = default);
    }
}
