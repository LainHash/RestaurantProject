using MediatR;
using Restaurant.Contract.DTOs.Catalog.IngredientCategories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.IngredientCategories.Commands.Update
{
    public record UpdateIngredientCategoryCommand(string Id, UpdateIngredientCategoryRequest Body)
        : IRequest<Result<IngredientCategoryResponse>>
    {
    }
}
