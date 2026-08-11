using MediatR;
using Restaurant.Contract.DTOs.Catalog.Ingredients;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Ingredients.Commands.Create
{
    public record CreateIngredientCommand(CreateIngredientRequest Body)
        : IRequest<Result<IngredientResponse>>
    {
    }
}
