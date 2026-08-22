using MediatR;
using Restaurant.Application.Services.Personnel;
using Restaurant.Contract.DTOs.Personnel.Departments;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Departments.Commands.Create
{
    internal class CreateDepartmentCommandHandler(IDepartmentService departmentService)
                : IRequestHandler<CreateDepartmentCommand, Result<DepartmentResponse>>
    {
        private readonly IDepartmentService _departmentService = departmentService;

        public async Task<Result<DepartmentResponse>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var response = await _departmentService.CreateAsync(request, cancellationToken);
            return response;
        }
    }
}
