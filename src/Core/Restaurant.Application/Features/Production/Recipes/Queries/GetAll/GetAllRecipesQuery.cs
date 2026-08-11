using MediatR;
using Restaurant.Contract.DTOs.Production.Recipes;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Production.Recipes.Queries.GetAll
{
    public record GetAllRecipesQuery()
        : IRequest<Result<IEnumerable<RecipeResponse>>>
    {
    }
}
