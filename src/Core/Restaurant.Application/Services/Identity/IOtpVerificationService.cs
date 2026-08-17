using Restaurant.Application.Features.Identity.OtpVerifications.Commands.ResendVerification;
using Restaurant.Application.Features.Identity.OtpVerifications.Commands.VerifyEmail;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Identity
{
    public interface IOtpVerificationService
    {
        Task InitializeAsync(User user, CancellationToken cancellationToken = default);

        Task<Result> VerifyEmailAsync(
            VerifyEmailCommand command,
            CancellationToken cancellationToken = default);

        Task<Result> ResendVerificationAsync(
            ResendVerificationCommand command,
            CancellationToken cancellationToken = default);
    }
}
