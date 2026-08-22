using MediatR;
using Restaurant.Contract.DTOs.Personnel.Departments;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Departments.Queries.GetById
{
    public record GetDepartmentByIdQuery(string Id)
        : IRequest<Result<DepartmentResponse>>
    {
    }
}
