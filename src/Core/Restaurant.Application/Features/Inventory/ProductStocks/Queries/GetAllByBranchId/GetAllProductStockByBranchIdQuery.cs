using MediatR;
using Restaurant.Application.Models.Results;
using Restaurant.Contract.DTOs.Inventory.ProductStocks;

namespace Restaurant.Application.Features.Inventory.ProductStocks.Queries.GetAllByBranchId
{
    public record GetAllProductStockByBranchIdQuery(string BranchId)
        : IRequest<Result<IEnumerable<ProductStockResponse>>>
    {
    }
}
