using MediatR;
using Restaurant.Application.Services.Personnel;
using Restaurant.Contract.DTOs.Personnel.Departments;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Departments.Queries.GetAll
{
    internal class GetAllDepartmentsQueryHandler(IDepartmentService departmentService)
        : IRequestHandler<GetAllDepartmentsQuery, PageResult<IEnumerable<DepartmentResponse>>>
    {
        private readonly IDepartmentService _departmentService = departmentService;

        public async Task<PageResult<IEnumerable<DepartmentResponse>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllDepartmentsSpecification(request);
            var response = await _departmentService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}
