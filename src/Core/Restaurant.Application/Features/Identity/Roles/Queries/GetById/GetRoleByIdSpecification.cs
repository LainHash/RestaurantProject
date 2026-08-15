using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Identity.Roles.Queries.GetById
{
    public class GetRoleByIdSpecification
        : BaseSpecification<Role>
    {
        public GetRoleByIdSpecification(GetRoleByIdQuery query)
        {
            EnableSoftDeleteFilter();

            Criteria = r => r.PublicId == query.Id;
        }
    }
}
