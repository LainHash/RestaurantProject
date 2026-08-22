using MediatR;
using Restaurant.Application.Services.Personnel;
using Restaurant.Contract.DTOs.Personnel.Departments;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Departments.Queries.GetById
{
    internal class GetDepartmentByIdQueryHandler(IDepartmentService departmentService)
                : IRequestHandler<GetDepartmentByIdQuery, Result<DepartmentResponse>>
    {
        private readonly IDepartmentService _departmentService = departmentService;

        public async Task<Result<DepartmentResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetDepartmentByIdSpecification(request);
            var response = await _departmentService.GetOneAsync(specification, cancellationToken);
            return response;
        }
    }
}
