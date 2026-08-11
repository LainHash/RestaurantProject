using Restaurant.Application.Features.Inventory.IngredientStocks.Commands.UpdateQuantity;
using Restaurant.Application.Features.Inventory.IngredientStocks.Queries.GetAllByBranchId;
using Restaurant.Application.Features.Inventory.IngredientStocks.Queries.GetAllByIngredientId;
using Restaurant.Contract.DTOs.Inventory.IngredientStocks;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Inventory
{
    public interface IIngredientStockService
    {
        Task<Result<IEnumerable<IngredientStockResponse>>> GetAllByIngredientIdAsync(
            GetAllIngredientStocksByIngredientIdQuery query,
            GetAllIngredientStocksByIngredientIdSpecification specification,
            CancellationToken cancellationToken);

        Task<Result<IEnumerable<IngredientStockResponse>>> GetAllByBranchIdAsync(
            GetAllIngredientStockByBranchIdQuery query,
            GetAllIngredientStockByBranchIdSpecification specification,
            CancellationToken cancellationToken);

        Task<Result<IngredientStockResponse>> UpdateQuantityAsync(
            UpdateIngredientStockQuantityCommand command,
            UpdateIngredientStockQuantitySpecification specification,
            CancellationToken cancellationToken);
    }
}
