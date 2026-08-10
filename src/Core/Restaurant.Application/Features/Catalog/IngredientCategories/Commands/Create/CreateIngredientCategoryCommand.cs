using MediatR;
using Restaurant.Contract.DTOs.Catalog.IngredientCategories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.IngredientCategories.Commands.Create
{
    public record CreateIngredientCategoryCommand(CreateIngredientCategoryRequest Body)
        : IRequest<Result<IngredientCategoryResponse>>
    {
    }
}
