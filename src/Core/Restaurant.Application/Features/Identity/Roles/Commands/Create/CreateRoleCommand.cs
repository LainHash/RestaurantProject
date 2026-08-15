using MediatR;
using Restaurant.Contract.DTOs.Identity.Roles;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.Roles.Commands.Create
{
    public record CreateRoleCommand(CreateRoleRequest Body)
        : IRequest<Result<RoleResponse>>
    {
    }
}
