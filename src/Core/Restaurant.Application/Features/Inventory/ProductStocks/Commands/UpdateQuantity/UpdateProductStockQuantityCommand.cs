using MediatR;
using Restaurant.Contract.DTOs.Inventory.ProductStocks;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.ProductStocks.Commands.UpdateQuantity
{
    public record UpdateProductStockQuantityCommand(string ProductId, string BranchId, UpdateProductStockQuantityRequest Body)
        : IRequest<Result<ProductStockResponse>>
    {
    }
}
