using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.ProductCategories.Commands.Delete
{
    public record DeleteProductCategoryCommand(string Id)
        : IRequest<Result>
    {
    }
}
