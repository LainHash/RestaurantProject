using MediatR;
using Restaurant.Contract.DTOs.Personnel.Departments;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Departments.Commands.Create
{
    public record CreateDepartmentCommand(CreateDepartmentRequest Body)
        : IRequest<Result<DepartmentResponse>>
    {
    }
}
