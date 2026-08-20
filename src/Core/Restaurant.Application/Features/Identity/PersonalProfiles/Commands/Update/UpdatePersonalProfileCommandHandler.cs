using MediatR;
using Restaurant.Application.Services.Identity;
using Restaurant.Contract.DTOs.Identity.PersonalProfiles;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.PersonalProfiles.Commands.Update
{
    internal class UpdatePersonalProfileCommandHandler(IPersonalProfileService personalProfileService)
                : IRequestHandler<UpdatePersonalProfileCommand, Result<PersonalProfileResponse>>
    {
        private readonly IPersonalProfileService _personalProfileService = personalProfileService;

        public async Task<Result<PersonalProfileResponse>> Handle(UpdatePersonalProfileCommand request, CancellationToken cancellationToken)
        {
            var specification = new UpdatePersonalProfileSpecification(request);
            var response = await _personalProfileService.UpdatePersonalProfileAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
