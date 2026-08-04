using MediatR;
using Restaurant.Application.Models.Results;

namespace Restaurant.Application.Features.Catalog.Brands.Commands.Delete
{
    public record DeleteBrandCommand(string Id)
        : IRequest<Result<object>>
    {
    }
}
