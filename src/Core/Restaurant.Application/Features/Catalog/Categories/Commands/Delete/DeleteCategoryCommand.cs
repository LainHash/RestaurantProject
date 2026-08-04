using MediatR;
using Restaurant.Application.Models.Results;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Delete
{
    public record DeleteCategoryCommand(string Id)
        : IRequest<Result<object>>
    {
    }
}
