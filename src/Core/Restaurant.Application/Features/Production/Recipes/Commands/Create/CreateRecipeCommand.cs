using MediatR;
using Restaurant.Contract.DTOs.Production.Recipes;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Production.Recipes.Commands.Create
{
    public record CreateRecipeCommand(CreateRecipeRequest Body)
        : IRequest<Result<RecipeResponse>>
    {
    }
}
