using AutoMapper;
using Restaurant.Application.Models.Messages;
using Restaurant.Application.Models.Results;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Categories;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Persistence.Services.Catalog
{
    internal class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IMapper _mapper;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
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
                    .Fail(Error<Category>.NotFound);
            }

            var response = _mapper.Map<CategoryResponse>(category);
            return Result<CategoryResponse>
                .Succeed(response, Success<Category>.Retrieved);
        }
    }
}
