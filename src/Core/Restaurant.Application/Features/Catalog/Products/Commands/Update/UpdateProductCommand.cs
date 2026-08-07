using MediatR;
using Restaurant.Contract.DTOs.Catalog.Products;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Update
{
    public record UpdateProductCommand(string Id, UpdateProductRequest Body)
        : IRequest<Result<ProductResponse>>
    {
    }
}
