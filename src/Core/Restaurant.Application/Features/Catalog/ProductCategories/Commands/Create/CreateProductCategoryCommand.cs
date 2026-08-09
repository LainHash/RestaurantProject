using MediatR;
using Restaurant.Contract.DTOs.Catalog.Categories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.ProductCategories.Commands.Create
{
    public record CreateProductCategoryCommand(CreateProductCategoryRequest Body)
        : IRequest<Result<ProductCategoryResponse>>
    {
    }
}
