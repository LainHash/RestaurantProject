using Restaurant.Application.Features.Identity.Roles.Commands.Create;
using Restaurant.Application.Features.Identity.Roles.Queries.GetAll;
using Restaurant.Application.Features.Identity.Roles.Queries.GetById;
using Restaurant.Contract.DTOs.Identity.Roles;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Identity
{
    public interface IRoleService
    {
        Task<Result<IEnumerable<RoleResponse>>> GetAllAsync(
            GetAllRolesSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<RoleResponse>> GetByIdAsync(
            GetRoleByIdSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<RoleResponse>> CreateAsync(
            CreateRoleCommand command,
            CancellationToken cancellationToken = default);
    }
}
