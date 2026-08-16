using MediatR;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.VerifyEmail
{
    public record VerifyEmailCommand(VerifyEmailRequest Body)
        : IRequest<Result>
    {
    }
}
