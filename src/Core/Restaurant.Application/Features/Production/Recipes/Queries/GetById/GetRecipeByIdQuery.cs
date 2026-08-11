using MediatR;
using Restaurant.Contract.DTOs.Production.Recipes;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Production.Recipes.Queries.GetById
{
    public record GetRecipeByIdQuery(string Id)
        : IRequest<Result<RecipeResponse>>
    {
    }
}
