using MediatR;
using Restaurant.Contract.DTOs.Inventory.IngredientStocks;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.IngredientStocks.Commands.UpdateQuantity
{
    public record UpdateIngredientStockQuantityCommand(string IngredientId, string BranchId, UpdateIngredientStockQuantityRequest Body)
        : IRequest<Result<IngredientStockResponse>>
    {
    }
}
