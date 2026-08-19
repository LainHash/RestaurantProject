using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Personnel.Departments.Queries.GetById
{
    public class GetDepartmentByIdSpecification
        : BaseSpecification<Department>
    {
        public GetDepartmentByIdSpecification(GetDepartmentByIdQuery query)
        {
            Criteria = department => string.Equals(department.PublicId, query.Id);

            EnableSoftDeleteFilter();
        }
    }
}
