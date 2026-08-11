using Restaurant.Application.Features.Production.Recipes.Queries.GetAll;
using Restaurant.Application.Features.Production.Recipes.Queries.GetById;
using Restaurant.Contract.DTOs.Production.Recipes;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Production
{
    public interface IRecipeService
    {
        Task<Result<IEnumerable<RecipeResponse>>> GetAllAsync(
            GetAllRecipesSpecification specification,
            CancellationToken cancellationToken);

        Task<Result<RecipeResponse>> GetByIdAsync(
            GetRecipeByIdSpecification specification,
            CancellationToken cancellationToken);
    }
}
