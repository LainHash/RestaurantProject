using MediatR;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.OtpVerifications.Commands.VerifyEmail
{
    public record VerifyEmailCommand(VerifyEmailRequest Body)
        : IRequest<Result>
    {
    }
}
