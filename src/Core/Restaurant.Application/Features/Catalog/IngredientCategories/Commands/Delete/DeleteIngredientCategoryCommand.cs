using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.IngredientCategories.Commands.Delete
{
    public record DeleteIngredientCategoryCommand(string Id)
        : IRequest<Result>
    {
    }
}
