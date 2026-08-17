using AutoMapper;
using Restaurant.Application.Features.Identity.PersonalProfiles.Commands.CompleteProfile;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Identity;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Identity;
using System.Net;

namespace Restaurant.Persistence.Services.Identity
{
    internal class PersonalProfileService : IPersonalProfileService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPersonalProfileRepository _personalProfileRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PersonalProfileService(
            IUserRepository userRepository,
            IPersonalProfileRepository personalProfileRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _personalProfileRepository = personalProfileRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> CompleteProfileAsync(
            CompleteProfileCommand command,
            CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByEmailAsync(command.Body.Email, cancellationToken);
            if (user is null)
            {
                return Result
                    .Fail(Error<User>.NotFound, HttpStatusCode.NotFound);
            }

            if (!user.IsActive)
            {
                return Result
                    .Fail("Account is not active. Please verify your email first.", HttpStatusCode.PreconditionRequired);
            }

            var personalProfile = await _personalProfileRepository.FindByUserAsync(user.Id, cancellationToken);
            if (personalProfile is not null)
            {
                return Result
                    .Fail("Profile has already been completed.", HttpStatusCode.Conflict);
            }

            personalProfile = _mapper.Map<PersonalProfile>(command.Body)
                .SetUser(user.Id);
            _personalProfileRepository.Add(personalProfile);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed("Profile completed successfully.", HttpStatusCode.Accepted);
        }
    }
}
