using Restaurant.Application.Features.Identity.PersonalProfiles.Commands.CompleteProfile;
using Restaurant.Application.Features.Identity.PersonalProfiles.Commands.Update;
using Restaurant.Contract.DTOs.Identity.PersonalProfiles;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Identity
{
    public interface IPersonalProfileService
    {
        Task<Result> CompleteProfileAsync(
            CompleteProfileCommand command,
            CancellationToken cancellationToken = default);

        Task<Result<PersonalProfileResponse>> UpdatePersonalProfileAsync(
            UpdatePersonalProfileCommand command,
            UpdatePersonalProfileSpecification specification,
            CancellationToken cancellationToken = default);
    }
}
