using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.Roles.Commands.Restore
{
    public record RestoreRoleCommand(string Id)
        : IRequest<Result>
    {
    }
}
