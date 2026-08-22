using MediatR;
using Restaurant.Application.Services.Personnel;
using Restaurant.Contract.DTOs.Personnel.Departments;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Departments.Queries.GetByName
{
    internal class GetDepartmentByNameQueryHandler(IDepartmentService departmentService)
                : IRequestHandler<GetDepartmentByNameQuery, Result<DepartmentResponse>>
    {
        private readonly IDepartmentService _departmentService = departmentService;

        public async Task<Result<DepartmentResponse>> Handle(GetDepartmentByNameQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetDepartmentByNameSpecification(request);
            var response = await _departmentService.GetOneAsync(specification, cancellationToken);
            return response;
        }
    }
}
