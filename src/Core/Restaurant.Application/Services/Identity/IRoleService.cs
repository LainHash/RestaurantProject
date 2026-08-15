using Restaurant.Application.Features.Identity.Roles.Queries.GetAll;
using Restaurant.Contract.DTOs.Identity.Roles;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Identity
{
    public interface IRoleService
    {
        Task<Result<IEnumerable<RoleResponse>>> GetAllAsync(
            GetAllRolesSpecification specification,
            CancellationToken cancellationToken = default);
    }
}
