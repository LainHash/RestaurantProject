using MediatR;
using Restaurant.Contract.DTOs.Personnel.Departments;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Departments.Commands.Update
{
    public record UpdateDepartmentCommand(string Id, UpdateDepartmentRequest Body)
        : IRequest<Result<DepartmentResponse>>
    {
    }
}
