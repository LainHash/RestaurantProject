using AutoMapper;
using Restaurant.Application.Features.Catalog.Categories.Commands.Create;
using Restaurant.Application.Features.Catalog.Categories.Commands.Update;
using Restaurant.Application.Models.Messages;
using Restaurant.Application.Models.Results;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Categories;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Specifications;
using System.Net;

namespace Restaurant.Persistence.Services.Catalog
{
    internal class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PageResult<IEnumerable<CategoryResponse>>> GetAllAsync(
            ISpecification<Category> specification,
            CancellationToken cancellationToken)
        {
            var totalItems = await _categoryRepository.CountAsync(specification, cancellationToken);

            var categories = await _categoryRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<CategoryResponse>>(categories);
            return PageResult<IEnumerable<CategoryResponse>>
                .Succeed(response, Success<Category>.Retrieved, totalItems, specification.Skip, specification.Take);

        }

        public async Task<Result<CategoryResponse>> GetOneAsync(
            ISpecification<Category> specification,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(specification, cancellationToken);
            if (category == null)
            {
                return Result<CategoryResponse>
                    .Fail(Error<Category>.NotFound, HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<CategoryResponse>(category);
            return Result<CategoryResponse>
                .Succeed(response, Success<Category>.Retrieved);
        }

        public async Task<Result<CategoryResponse>> CreateAsync(
            CreateCategoryCommand command,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindByNameAsync(command.Body.Name, cancellationToken);
            if(category is not null)
            {
                return Result<CategoryResponse>
                    .Fail(Error<Category>.ExistedName, HttpStatusCode.Conflict);
            }

            category = _mapper.Map<Category>(command.Body);
            _categoryRepository.Add(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<CategoryResponse>(category);
            return Result<CategoryResponse>
                .Succeed(response, Success<Category>.Created, HttpStatusCode.Created);
        }

        public async Task<Result<CategoryResponse>> UpdateAsync(
            UpdateCategoryCommand command,
            UpdateCategorySpecification specification,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(specification, cancellationToken);
            if (category is null)
            {
                return Result<CategoryResponse>
                    .Fail(Error<Category>.NotFound, HttpStatusCode.NotFound);
            }

            if(await _categoryRepository.IsExistingNameAsync(command.Body.Name, cancellationToken))
            {
                return Result<CategoryResponse>
                    .Fail(Error<Category>.ExistedName, HttpStatusCode.Conflict);
            }

            _mapper.Map(command.Body, category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<CategoryResponse>(category);
            return Result<CategoryResponse>
                .Succeed(response, Success<Category>.Updated, HttpStatusCode.OK);
        }

        public async Task<Result<object>> DeleteAsync(
            ISpecification<Category> specification,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(specification, cancellationToken);
            if (category == null)
            {
                return Result<object>
                    .Fail(Error<Category>.NotFound, HttpStatusCode.NotFound);
            }

            if(category.IsDeleted)
            {
                return Result<object>
                    .Fail(Error<Category>.AlreadyDeleted, HttpStatusCode.BadRequest);
            }

            category.SoftDelete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<object>
                .Succeed(default, Success<Category>.Deleted);
        }

        public async Task<Result<object>> RestoreAsync(
            ISpecification<Category> specification,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(specification, cancellationToken);
            if (category == null)
            {
                return Result<object>
                    .Fail(Error<Category>.NotFound, HttpStatusCode.NotFound);
            }

            if(!category.IsDeleted)
            {
                return Result<object>
                    .Fail(Error<Category>.NotYetDeleted, HttpStatusCode.BadRequest);
            }

            category.Restore();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<object>
                .Succeed(default, Success<Category>.Restored);
        }
    }
}
