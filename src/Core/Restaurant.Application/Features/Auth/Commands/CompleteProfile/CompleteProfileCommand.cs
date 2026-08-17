using MediatR;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.CompleteProfile
{
    public record CompleteProfileCommand(CompleteProfileRequest Body)
        : IRequest<Result>
    {
    }
}
