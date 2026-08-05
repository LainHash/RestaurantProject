using MediatR;
using Restaurant.Application.Models.Results;
using Restaurant.Contract.DTOs.Catalog.Products;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Create
{
    public record CreateProductCommand(CreateProductRequest Body)
        : IRequest<Result<ProductResponse>>
    {
    }
}
