using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Personnel.Departments.Commands.Update
{
    public class UpdateDepartmentSpecification
        : BaseSpecification<Department>
    {
        public UpdateDepartmentSpecification(UpdateDepartmentCommand command)
        {
            Criteria = d => string.Equals(d.PublicId, command.Id);
        }
    }
}
