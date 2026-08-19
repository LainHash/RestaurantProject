using MediatR;
using Restaurant.Application.Services.Personnel;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Departments.Commands.Delete
{
    internal class DeleteDepartmentCommandHandler(IDepartmentService departmentService)
                : IRequestHandler<DeleteDepartmentCommand, Result>
    {
        private readonly IDepartmentService _departmentService = departmentService;

        public async Task<Result> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var specification = new DeleteDepartmentSpecification(request);
            var response = await _departmentService.DeleteAsync(specification, cancellationToken);
            return response;
        }
    }
}
