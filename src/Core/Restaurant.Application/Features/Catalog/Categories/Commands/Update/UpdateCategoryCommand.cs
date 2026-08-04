using MediatR;
using Restaurant.Application.Models.Results;
using Restaurant.Contract.DTOs.Catalog.Categories;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Update
{
    public record UpdateCategoryCommand(string Id, UpdateCategoryRequest Body)
        : IRequest<Result<CategoryResponse>>
    {
    }
}
