using Restaurant.Application.Features.Catalog.Categories.Commands.Create;
using Restaurant.Application.Features.Catalog.Categories.Commands.Update;
using Restaurant.Application.Models.Results;
using Restaurant.Contract.DTOs.Catalog.Categories;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Services.Catalog
{
    public interface ICategoryService
    {
        Task<PageResult<IEnumerable<CategoryResponse>>> GetAllAsync(
            ISpecification<Category> specification,
            CancellationToken cancellationToken);

        Task<Result<CategoryResponse>> GetOneAsync(
            ISpecification<Category> specification,
            CancellationToken cancellationToken);

        Task<Result<CategoryResponse>> CreateAsync(
            CreateCategoryCommand command,
            CancellationToken cancellationToken);

        Task<Result<CategoryResponse>> UpdateAsync(
            UpdateCategoryCommand command,
            UpdateCategorySpecification specification,
            CancellationToken cancellationToken);

        Task<Result<object>> DeleteAsync(
            ISpecification<Category> specification,
            CancellationToken cancellationToken);

        Task<Result<object>> RestoreAsync(
            ISpecification<Category> specification,
            CancellationToken cancellationToken);
    }
}
