using MediatR;
using Restaurant.Application.Services.Identity;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.PersonalProfiles.Commands.CompleteProfile
{
    internal class CompleteProfileCommandHandler(IPersonalProfileService personalProfileService)
                : IRequestHandler<CompleteProfileCommand, Result>
    {
        private readonly IPersonalProfileService _personalProfileService = personalProfileService;

        public async Task<Result> Handle(CompleteProfileCommand request, CancellationToken cancellationToken)
        {
            var response = await _personalProfileService.CompleteProfileAsync(request, cancellationToken);
            return response;
        }
    }
}
