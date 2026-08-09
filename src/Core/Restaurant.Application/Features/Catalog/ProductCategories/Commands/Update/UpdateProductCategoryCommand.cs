using MediatR;
using Restaurant.Contract.DTOs.Catalog.Categories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.ProductCategories.Commands.Update
{
    public record UpdateProductCategoryCommand(string Id, UpdateProductCategoryRequest Body)
        : IRequest<Result<ProductCategoryResponse>>
    {
    }
}
