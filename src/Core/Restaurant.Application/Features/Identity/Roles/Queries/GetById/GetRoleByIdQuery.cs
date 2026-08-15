using MediatR;
using Restaurant.Contract.DTOs.Identity.Roles;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.Roles.Queries.GetById
{
    public record GetRoleByIdQuery(string Id)
        : IRequest<Result<RoleResponse>>
    {
    }
}
