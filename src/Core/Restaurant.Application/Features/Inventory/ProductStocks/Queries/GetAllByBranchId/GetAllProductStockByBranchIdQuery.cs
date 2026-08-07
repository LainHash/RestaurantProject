using MediatR;
using Restaurant.Contract.DTOs.Inventory.ProductStocks;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.ProductStocks.Queries.GetAllByBranchId
{
    public record GetAllProductStockByBranchIdQuery(string BranchId)
        : IRequest<Result<IEnumerable<ProductStockResponse>>>
    {
    }
}
