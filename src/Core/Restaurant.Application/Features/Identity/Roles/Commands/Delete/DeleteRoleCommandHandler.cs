using MediatR;
using Restaurant.Application.Services.Identity;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.Roles.Commands.Delete
{
    internal class DeleteRoleCommandHandler(IRoleService roleService)
                : IRequestHandler<DeleteRoleCommand, Result>
    {
        private readonly IRoleService _roleService = roleService;

        public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var specification = new DeleteRoleSpecification(request);
            var response = await _roleService.DeleteAsync(specification, cancellationToken);
            return response;
        }
    }
}
