using MediatR;
using Restaurant.Application.Models.Results;
using Restaurant.Contract.DTOs.Catalog.Products;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Update
{
    public record UpdateProductCommand(string Id, UpdateProductRequest Body)
        : IRequest<Result<ProductResponse>>
    {
    }
}
