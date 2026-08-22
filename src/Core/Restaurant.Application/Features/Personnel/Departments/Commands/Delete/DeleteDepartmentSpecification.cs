using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Personnel.Departments.Commands.Delete
{
    public class DeleteDepartmentSpecification
        : BaseSpecification<Department>
    {
        public DeleteDepartmentSpecification(DeleteDepartmentCommand command)
        {
            Criteria = department => string.Equals(department.PublicId, command.Id);
        }
    }
}
