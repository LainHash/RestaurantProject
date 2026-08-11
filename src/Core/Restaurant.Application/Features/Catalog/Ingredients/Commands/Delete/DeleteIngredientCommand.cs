using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Ingredients.Commands.Delete
{
    public record DeleteIngredientCommand(string Id)
        : IRequest<Result<object>>
    {
    }
}
