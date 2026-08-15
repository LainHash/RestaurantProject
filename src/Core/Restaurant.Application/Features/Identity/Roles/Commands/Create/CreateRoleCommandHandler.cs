using MediatR;
using Restaurant.Application.Services.Identity;
using Restaurant.Contract.DTOs.Identity.Roles;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.Roles.Commands.Create
{
    internal class CreateRoleCommandHandler(IRoleService roleService)
                : IRequestHandler<CreateRoleCommand, Result<RoleResponse>>
    {
        private readonly IRoleService _roleService = roleService;

        public async Task<Result<RoleResponse>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var response = await _roleService.CreateAsync(request, cancellationToken);
            return response;
        }
    }
}
