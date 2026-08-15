using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Identity.Roles.Commands.Update
{
    public class UpdateRoleSpecification
        : BaseSpecification<Role>
    {
        public UpdateRoleSpecification(UpdateRoleCommand command)
        {
            Criteria = r => r.PublicId == command.Id;
        }
    }
}
