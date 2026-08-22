using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Personnel.Departments.Queries.GetByName
{
    public class GetDepartmentByNameSpecification
        : BaseSpecification<Department>
    {
        public GetDepartmentByNameSpecification(GetDepartmentByNameQuery query)
        {
            Criteria = department => string.Equals(department.Name, query.Name);
            EnableSoftDeleteFilter();
        }
    }
}
