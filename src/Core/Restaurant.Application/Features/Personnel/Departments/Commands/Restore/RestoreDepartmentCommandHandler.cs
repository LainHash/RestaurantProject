using MediatR;
using Restaurant.Application.Services.Personnel;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Departments.Commands.Restore
{
    internal class RestoreDepartmentCommandHandler(IDepartmentService departmentService)
                : IRequestHandler<RestoreDepartmentCommand, Result>
    {
        private readonly IDepartmentService _departmentService = departmentService;

        public async Task<Result> Handle(RestoreDepartmentCommand request, CancellationToken cancellationToken)
        {
            var specification = new RestoreDepartmentSpecification(request);
            var response = await _departmentService.RestoreAsync(specification, cancellationToken);
            return response;
        }
    }
}
