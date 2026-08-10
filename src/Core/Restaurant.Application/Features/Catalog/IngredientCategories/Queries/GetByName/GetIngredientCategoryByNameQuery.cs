using MediatR;
using Restaurant.Contract.DTOs.Catalog.IngredientCategories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.IngredientCategories.Queries.GetByName
{
    public record GetIngredientCategoryByNameQuery(string Name)
         : IRequest<Result<IngredientCategoryResponse>>
    {
    }
}
