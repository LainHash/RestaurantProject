using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Delete
{
    public record DeleteProductCommand(string Id)
        : IRequest<Result<object>>
    {
    }
}
