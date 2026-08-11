using Restaurant.Application.Features.Catalog.Ingredients.Commands.Create;
using Restaurant.Application.Features.Catalog.Ingredients.Commands.Update;
using Restaurant.Contract.DTOs.Catalog.Ingredients;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Services.Catalog
{
    public interface IIngredientService
    {
        Task<PageResult<IEnumerable<IngredientResponse>>> GetAllAsync(
            ISpecification<Ingredient> specification,
            CancellationToken cancellationToken);

        Task<Result<IngredientResponse>> GetByIdAsync(
            ISpecification<Ingredient> specification,
            CancellationToken cancellationToken);

        Task<Result<IngredientResponse>> CreateAsync(
            CreateIngredientSpecification specification,
            CreateIngredientRequest request,
            CancellationToken cancellationToken);

        Task<Result<IngredientResponse>> UpdateAsync(
            UpdateIngredientSpecification specification,
            UpdateIngredientRequest request,
            CancellationToken cancellationToken);

        Task<Result<object>> DeleteAsync(
            ISpecification<Ingredient> specification,
            CancellationToken cancellationToken);

        Task<Result<object>> RestoreAsync(
            ISpecification<Ingredient> specification,
            CancellationToken cancellationToken);
    }
}
