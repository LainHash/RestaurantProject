using MediatR;
using Restaurant.Application.Services.Personnel;
using Restaurant.Contract.DTOs.Personnel.Departments;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Departments.Commands.Update
{
    internal class UpdateDepartmentCommandHandler(IDepartmentService departmentService)
                : IRequestHandler<UpdateDepartmentCommand, Result<DepartmentResponse>>
    {
        private readonly IDepartmentService _departmentService = departmentService;

        public async Task<Result<DepartmentResponse>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var specification = new UpdateDepartmentSpecification(request);
            var response = await _departmentService.UpdateAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
