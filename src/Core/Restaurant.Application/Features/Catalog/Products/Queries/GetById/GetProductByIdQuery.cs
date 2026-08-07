using MediatR;
using Restaurant.Contract.DTOs.Catalog.Products;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetById
{
    public record GetProductByIdQuery(string Id) 
        : IRequest<Result<ProductResponse>>
    {
    }
}
