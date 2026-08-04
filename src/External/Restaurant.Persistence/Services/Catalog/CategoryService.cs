using AutoMapper;
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

        public async Task<Result<IEnumerable<CategoryResponse>>> GetAllAsync(
            ISpecification<Category> specification,
            CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.ToListAsync(specification, cancellationToken);
            if (!categories.Any())
            {
                return Result<IEnumerable<CategoryResponse>>
                    .Fail(Error<Category>.EmptyList);
            }

            var response = _mapper.Map<IEnumerable<CategoryResponse>>(categories);
            return Result<IEnumerable<CategoryResponse>>
                .Succeed(response, Success<Category>.Retrieved);

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
            Category category,
            CancellationToken cancellationToken)
        {
            _categoryRepository.Add(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<CategoryResponse>(category);
            return Result<CategoryResponse>
                .Succeed(response, Success<Category>.Created, HttpStatusCode.Created);
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
                .Succeed(default, Success<Category>.Deleted, HttpStatusCode.Accepted);
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
                .Succeed(default, Success<Category>.Restored, HttpStatusCode.Accepted);
        }
    }
}
