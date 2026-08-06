using MediatR;
using Restaurant.Application.Models.Results;
using Restaurant.Contract.DTOs.Inventory.ProductStocks;

namespace Restaurant.Application.Features.Inventory.ProductStocks.Commands.UpdateQuantity
{
    public record UpdateProductStockQuantityCommand(string ProductId, string BranchId, UpdateProductStockQuantityRequest Body)
        : IRequest<Result<ProductStockResponse>>
    {
    }
}
