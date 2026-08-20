using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Identity.PersonalProfiles.Commands.Update
{
    public class UpdatePersonalProfileSpecification
        : BaseSpecification<PersonalProfile>
    {
        public UpdatePersonalProfileSpecification(UpdatePersonalProfileCommand command)
        {
            AddInclude(x => x.User);

            Criteria = pp => pp.User.PublicId == command.UserId;
        }
    }
}
