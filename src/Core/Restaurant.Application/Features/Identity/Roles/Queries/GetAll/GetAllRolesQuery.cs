using MediatR;
using Restaurant.Contract.DTOs.Identity.Roles;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.Roles.Queries.GetAll
{
    public record GetAllRolesQuery()
        : IRequest<Result<IEnumerable<RoleResponse>>>
    {
    }
}
