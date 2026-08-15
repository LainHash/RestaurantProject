using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Identity.Roles.Queries.GetAll
{
    public class GetAllRolesSpecification
        : BaseSpecification<Role>
    {
        public GetAllRolesSpecification(GetAllRolesQuery query)
        {
            EnableSoftDeleteFilter();
        }
    }
}
