using MediatR;
using Restaurant.Application.Services.Identity;
using Restaurant.Contract.DTOs.Identity.Roles;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.Roles.Queries.GetAll
{
    internal class GetAllRolesQueryHandler(IRoleService roleService)
                : IRequestHandler<GetAllRolesQuery, Result<IEnumerable<RoleResponse>>>
    {
        private readonly IRoleService _roleService = roleService;

        public async Task<Result<IEnumerable<RoleResponse>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllRolesSpecification(request);
            var response = await _roleService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}
