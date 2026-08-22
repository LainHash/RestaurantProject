using MediatR;
using Restaurant.Contract.DTOs.Personnel.Departments;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Departments.Queries.GetByName
{
    public record GetDepartmentByNameQuery(string Name)
         : IRequest<Result<DepartmentResponse>>
    {
    }
}
