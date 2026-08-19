using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Departments.Commands.Restore
{
    public record RestoreDepartmentCommand(string Id)
        : IRequest<Result>
    {
    }
}
