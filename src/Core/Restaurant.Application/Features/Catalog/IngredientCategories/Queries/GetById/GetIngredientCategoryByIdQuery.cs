using MediatR;
using Restaurant.Contract.DTOs.Catalog.IngredientCategories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.IngredientCategories.Queries.GetById
{
    public record GetIngredientCategoryByIdQuery(string Id)
        : IRequest<Result<IngredientCategoryResponse>>
    {
    }
}
