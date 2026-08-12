using MediatR;
using Restaurant.Contract.DTOs.Production.RecipeIngredients;
using Restaurant.Contract.DTOs.Production.Recipes;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Production.Recipes.Commands.AddIngredient
{
    public record AddRecipeIngredientCommand(string Id, IEnumerable<AddRecipeIngredientRequest> Body)
        : IRequest<Result<RecipeResponse>>
    {
    }
}
