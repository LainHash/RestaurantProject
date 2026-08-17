using Restaurant.Application.Features.Identity.PersonalProfiles.Commands.CompleteProfile;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Identity
{
    public interface IPersonalProfileService
    {
        Task<Result> CompleteProfileAsync(
            CompleteProfileCommand command,
            CancellationToken cancellationToken = default);
    }
}
