using MediatR;
using Restaurant.Application.Services.Identity;
using Restaurant.Contract.DTOs.Identity.Roles;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.Roles.Commands.Update
{
    internal class UpdateRoleCommandHandler(IRoleService roleService)
                : IRequestHandler<UpdateRoleCommand, Result<RoleResponse>>
    {
        private readonly IRoleService _roleService = roleService;

        public async Task<Result<RoleResponse>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var specification = new UpdateRoleSpecification(request);
            var response = await _roleService.UpdateAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
