using MediatR;
using Restaurant.Contract.DTOs.Catalog.Ingredients;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Ingredients.Queries.GetById
{
    public record GetIngredientByIdQuery(string Id)
        : IRequest<Result<IngredientResponse>>
    {
    }
}
