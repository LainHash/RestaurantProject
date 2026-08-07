using MediatR;
using Restaurant.Contract.DTOs.Catalog.Products;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Create
{
    public record CreateProductCommand(CreateProductRequest Body)
        : IRequest<Result<ProductResponse>>
    {
    }
}
