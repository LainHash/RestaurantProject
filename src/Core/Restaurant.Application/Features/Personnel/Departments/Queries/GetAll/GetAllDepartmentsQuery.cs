using MediatR;
using Restaurant.Contract.DTOs.Personnel.Departments;
using Restaurant.Domain.Models;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Departments.Queries.GetAll
{
    public record GetAllDepartmentsQuery
        : PageQuery, IRequest<PageResult<IEnumerable<DepartmentResponse>>>
    {
    }
}
