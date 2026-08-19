using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Departments.Commands.Delete
{
    public record DeleteDepartmentCommand(string Id)
        : IRequest<Result>
    {
    }
}
