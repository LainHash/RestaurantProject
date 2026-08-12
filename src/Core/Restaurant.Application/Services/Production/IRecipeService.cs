using Restaurant.Application.Features.Production.Recipes.Commands.AddIngredient;
using Restaurant.Application.Features.Production.Recipes.Commands.Create;
using Restaurant.Application.Features.Production.Recipes.Commands.Update;
using Restaurant.Application.Features.Production.Recipes.Queries.GetAll;
using Restaurant.Application.Features.Production.Recipes.Queries.GetAllByProductId;
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

        Task<Result<IEnumerable<RecipeResponse>>> GetAllByProductIdAsync(
            GetAllRecipesByProductIdQuery query,
            GetAllRecipesByProductIdSpecification specification,
            CancellationToken cancellationToken);

        Task<Result<RecipeResponse>> CreateAsync(
            CreateRecipeCommand command,
            CreateRecipeSpecification specification,
            CancellationToken cancellationToken);

        Task<Result<RecipeResponse>> UpdateAsync(
            UpdateRecipeCommand command,
            UpdateRecipeSpecification specification,
            CancellationToken cancellationToken);

        Task<Result<RecipeResponse>> AddIngredientAsync(
            AddRecipeIngredientCommand command,
            AddRecipeIngredientSpecification specification,
            CancellationToken cancellationToken);
    }
}
