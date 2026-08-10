using Restaurant.Application.Features.Catalog.IngredientCategories.Commands.Create;
using Restaurant.Application.Features.Catalog.IngredientCategories.Commands.Update;
using Restaurant.Contract.DTOs.Catalog.IngredientCategories;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Services.Catalog
{
    public interface IIngredientCategoryService
    {
        Task<PageResult<IEnumerable<IngredientCategoryResponse>>> GetAllAsync(
            ISpecification<IngredientCategory> specification,
            CancellationToken cancellationToken);

        Task<Result<IngredientCategoryResponse>> GetOneAsync(
            ISpecification<IngredientCategory> specification,
            CancellationToken cancellationToken);

        Task<Result<IngredientCategoryResponse>> CreateAsync(
            CreateIngredientCategoryCommand command,
            CancellationToken cancellationToken);

        Task<Result<IngredientCategoryResponse>> UpdateAsync(
            UpdateIngredientCategoryCommand command,
            UpdateIngredientCategorySpecification specification,
            CancellationToken cancellationToken);

        Task<Result<object>> DeleteAsync(
            ISpecification<IngredientCategory> specification,
            CancellationToken cancellationToken);

        Task<Result<object>> RestoreAsync(
            ISpecification<IngredientCategory> specification,
            CancellationToken cancellationToken);
    }
}
