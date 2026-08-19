using Restaurant.Application.Features.Personnel.Departments.Commands.Create;
using Restaurant.Application.Features.Personnel.Departments.Commands.Update;
using Restaurant.Contract.DTOs.Personnel.Departments;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Services.Personnel
{
    public interface IDepartmentService
    {
        Task<PageResult<IEnumerable<DepartmentResponse>>> GetAllAsync(
            ISpecification<Department> specification,
            CancellationToken cancellationToken);

        Task<Result<DepartmentResponse>> GetOneAsync(
            ISpecification<Department> specification,
            CancellationToken cancellationToken);

        Task<Result<DepartmentResponse>> CreateAsync(
            CreateDepartmentCommand command,
            CancellationToken cancellationToken);

        Task<Result<DepartmentResponse>> UpdateAsync(
            UpdateDepartmentCommand command,
            UpdateDepartmentSpecification specification,
            CancellationToken cancellationToken);

        Task<Result> DeleteAsync(
            ISpecification<Department> specification,
            CancellationToken cancellationToken);

        Task<Result> RestoreAsync(
            ISpecification<Department> specification,
            CancellationToken cancellationToken);
    }
}
