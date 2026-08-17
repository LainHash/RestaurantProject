using MediatR;
using Restaurant.Application.Services.Identity;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.Roles.Commands.Restore
{
    internal class RestoreRoleCommandHandler(IRoleService roleService)
                : IRequestHandler<RestoreRoleCommand, Result>
    {
        private readonly IRoleService _roleService = roleService;

        public async Task<Result> Handle(RestoreRoleCommand request, CancellationToken cancellationToken)
        {
            var specification = new RestoreRoleSpecification(request);
            var response = await _roleService.RestoreAsync(specification, cancellationToken);
            return response;
        }
    }
}
