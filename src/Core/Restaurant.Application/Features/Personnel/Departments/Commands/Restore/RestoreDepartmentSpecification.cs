using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Personnel.Departments.Commands.Restore
{
    public class RestoreDepartmentSpecification
        : BaseSpecification<Department>
    {
        public RestoreDepartmentSpecification(RestoreDepartmentCommand command)
        {
            Criteria = department => string.Equals(department.PublicId, command.Id);
        }
    }
}
