using MediatR;
using Restaurant.Contract.DTOs.Production.Recipes;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Production.Recipes.Queries.GetAllByProductId
{
    public record GetAllRecipesByProductIdQuery(string ProductId)
        : IRequest<Result<IEnumerable<RecipeResponse>>>
    {
    }
}
