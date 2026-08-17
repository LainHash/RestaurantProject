using ConvenienceStore.Contract.DTOs.Authentication;
using Restaurant.Application.Features.Auth.Commands.CompleteProfile;
using Restaurant.Application.Features.Auth.Commands.Login;
using Restaurant.Application.Features.Auth.Commands.Register;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Auth
{
    public interface IAuthenticationService
    {
        Task<Result<AuthenticationResponse>> LoginAsync(
            LoginCommand command,
            CancellationToken cancellationToken = default);

        Task<Result> RegisterAsync(
            RegisterCommand command,
            CancellationToken cancellationToken = default);

        Task<Result> CompleteProfileAsync(
            CompleteProfileCommand command,
            CancellationToken cancellationToken = default);
    }
}
