using Restaurant.Application.Features.Identity.Roles.Commands.Create;
using Restaurant.Application.Features.Identity.Roles.Commands.Delete;
using Restaurant.Application.Features.Identity.Roles.Commands.Restore;
using Restaurant.Application.Features.Identity.Roles.Commands.Update;
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

        Task<Result<RoleResponse>> UpdateAsync(
            UpdateRoleCommand command,
            UpdateRoleSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result> DeleteAsync(
            DeleteRoleSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result> RestoreAsync(
            RestoreRoleSpecification specification,
            CancellationToken cancellationToken = default);
    }
}
