using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Identity.Roles.Commands.Delete
{
    public class DeleteRoleSpecification
        : BaseSpecification<Role>
    {
        public DeleteRoleSpecification(DeleteRoleCommand command)
        {
            Criteria = r => r.PublicId == command.Id;
        }
    }
}
