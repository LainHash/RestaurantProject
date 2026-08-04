using MediatR;
using Restaurant.Application.Models.Results;
using Restaurant.Contract.DTOs.Catalog.Categories;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Create
{
    public record CreateCategoryCommand(CreateCategoryRequest Body)
        : IRequest<Result<CategoryResponse>>
    {
    }
}
