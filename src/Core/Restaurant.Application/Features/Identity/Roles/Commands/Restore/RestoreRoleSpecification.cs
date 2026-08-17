using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Identity.Roles.Commands.Restore
{
    public class RestoreRoleSpecification
        : BaseSpecification<Role>
    {
        public RestoreRoleSpecification(RestoreRoleCommand command)
        {
            Criteria = r => r.PublicId == command.Id;
        }
    }
}
