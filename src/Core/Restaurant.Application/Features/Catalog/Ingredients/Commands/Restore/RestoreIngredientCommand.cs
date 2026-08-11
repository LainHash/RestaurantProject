using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Ingredients.Commands.Restore
{
    public record RestoreIngredientCommand(string Id)
        : IRequest<Result<object>>
    {
    }
}
