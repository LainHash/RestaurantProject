using AutoMapper;
using Restaurant.Application.Features.Personnel.Departments.Commands.Create;
using Restaurant.Application.Features.Personnel.Departments.Commands.Update;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Personnel;
using Restaurant.Contract.DTOs.Personnel.Departments;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Personnel;
using Restaurant.Domain.Specifications;
using System.Net;

namespace Restaurant.Persistence.Services.Personnel
{
    internal class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(
            IDepartmentRepository departmentRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PageResult<IEnumerable<DepartmentResponse>>> GetAllAsync(
            ISpecification<Department> specification,
            CancellationToken cancellationToken)
        {
            var totalItems = await _departmentRepository.CountAsync(specification, cancellationToken);

            var departments = await _departmentRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<DepartmentResponse>>(departments);
            return PageResult<IEnumerable<DepartmentResponse>>
                .Succeed(response, Success<Department>.Retrieved, totalItems, specification.Skip, specification.Take);
        }

        public async Task<Result<DepartmentResponse>> GetOneAsync(
            ISpecification<Department> specification,
            CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.FindAsync(specification, cancellationToken);
            if (department == null)
            {
                return Result<DepartmentResponse>
                    .Fail(Error<Department>.NotFound, HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<DepartmentResponse>(department);
            return Result<DepartmentResponse>
                .Succeed(response, Success<Department>.Retrieved);
        }

        public async Task<Result<DepartmentResponse>> CreateAsync(
            CreateDepartmentCommand command,
            CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.FindByNameAsync(command.Body.Name, cancellationToken);
            if (department is not null)
            {
                return Result<DepartmentResponse>
                    .Fail(Error<Department>.ExistedName, HttpStatusCode.Conflict);
            }

            department = _mapper.Map<Department>(command.Body);
            _departmentRepository.Add(department);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<DepartmentResponse>(department);
            return Result<DepartmentResponse>
                .Succeed(response, Success<Department>.Created, HttpStatusCode.Created);
        }

        public async Task<Result<DepartmentResponse>> UpdateAsync(
            UpdateDepartmentCommand command,
            UpdateDepartmentSpecification specification,
            CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.FindAsync(specification, cancellationToken);
            if (department is null)
            {
                return Result<DepartmentResponse>
                    .Fail(Error<Department>.NotFound, HttpStatusCode.NotFound);
            }

            if (await _departmentRepository.IsExistingNameAsync(command.Body.Name, cancellationToken))
            {
                return Result<DepartmentResponse>
                    .Fail(Error<Department>.ExistedName, HttpStatusCode.Conflict);
            }

            _mapper.Map(command.Body, department);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<DepartmentResponse>(department);
            return Result<DepartmentResponse>
                .Succeed(response, Success<Department>.Updated, HttpStatusCode.OK);
        }

        public async Task<Result> DeleteAsync(
            ISpecification<Department> specification,
            CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.FindAsync(specification, cancellationToken);
            if (department == null)
            {
                return Result
                    .Fail(Error<Department>.NotFound, HttpStatusCode.NotFound);
            }

            if (department.IsDeleted)
            {
                return Result
                    .Fail(Error<Department>.AlreadyDeleted, HttpStatusCode.BadRequest);
            }

            department.SoftDelete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success<Department>.Deleted);
        }

        public async Task<Result> RestoreAsync(
            ISpecification<Department> specification,
            CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.FindAsync(specification, cancellationToken);
            if (department == null)
            {
                return Result
                    .Fail(Error<Department>.NotFound, HttpStatusCode.NotFound);
            }

            if (!department.IsDeleted)
            {
                return Result
                    .Fail(Error<Department>.NotYetDeleted, HttpStatusCode.BadRequest);
            }

            department.Restore();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success<Department>.Restored);
        }
    }
}
