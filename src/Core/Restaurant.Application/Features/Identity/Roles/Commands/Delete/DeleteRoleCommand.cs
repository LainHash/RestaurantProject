using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.Roles.Commands.Delete
{
    public record DeleteRoleCommand(string Id)
        : IRequest<Result>
    {
    }
}
