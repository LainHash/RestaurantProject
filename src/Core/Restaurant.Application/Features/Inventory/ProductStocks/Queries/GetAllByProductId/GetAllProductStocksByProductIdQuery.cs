using MediatR;
using Restaurant.Contract.DTOs.Inventory.ProductStocks;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.ProductStocks.Queries.GetAllByProductId
{
    public record GetAllProductStocksByProductIdQuery(string ProductId)
        : IRequest<Result<IEnumerable<ProductStockResponse>>>
    {
    }
}
