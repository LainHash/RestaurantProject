using MediatR;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.ResendVerification
{
    public record ResendVerificationCommand(ResendVerificationRequest Body)
        : IRequest<Result>
    {
    }
}
