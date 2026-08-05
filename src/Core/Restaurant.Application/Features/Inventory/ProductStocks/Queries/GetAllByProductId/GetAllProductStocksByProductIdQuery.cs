using MediatR;
using Restaurant.Application.Models.Results;
using Restaurant.Contract.DTOs.Inventory.ProductStocks;

namespace Restaurant.Application.Features.Inventory.ProductStocks.Queries.GetAllByProductId
{
    public record GetAllProductStocksByProductIdQuery(string ProductId)
        : IRequest<Result<IEnumerable<ProductStockResponse>>>
    {
    }
}
