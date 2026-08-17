using MediatR;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.PersonalProfiles.Commands.CompleteProfile
{
    public record CompleteProfileCommand(CompleteProfileRequest Body)
        : IRequest<Result>
    {
    }
}
