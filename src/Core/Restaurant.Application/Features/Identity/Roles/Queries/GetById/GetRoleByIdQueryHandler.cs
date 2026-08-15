using MediatR;
using Restaurant.Application.Services.Identity;
using Restaurant.Contract.DTOs.Identity.Roles;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.Roles.Queries.GetById
{
    internal class GetRoleByIdQueryHandler(IRoleService roleService)
                : IRequestHandler<GetRoleByIdQuery, Result<RoleResponse>>
    {
        private readonly IRoleService _roleService = roleService;

        public async Task<Result<RoleResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetRoleByIdSpecification(request);
            var response = await _roleService.GetByIdAsync(specification, cancellationToken);
            return response;
        }
    }
}
