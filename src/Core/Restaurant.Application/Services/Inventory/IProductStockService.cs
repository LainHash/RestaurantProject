using Restaurant.Application.Features.Inventory.ProductStocks.Commands.UpdateQuantity;
using Restaurant.Application.Features.Inventory.ProductStocks.Queries.GetAllByBranchId;
using Restaurant.Application.Features.Inventory.ProductStocks.Queries.GetAllByProductId;
using Restaurant.Contract.DTOs.Inventory.ProductStocks;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Inventory
{
    public interface IProductStockService
    {
        Task<Result<IEnumerable<ProductStockResponse>>> GetAllByProductIdAsync(
            GetAllProductStocksByProductIdQuery query,
            GetAllProductStocksByProductIdSpecification specification,
            CancellationToken cancellationToken);

        Task<Result<IEnumerable<ProductStockResponse>>> GetAllByBranchIdAsync(
            GetAllProductStockByBranchIdQuery query,
            GetAllProductStockByBranchIdSpecification specification,
            CancellationToken cancellationToken);

        Task<Result<ProductStockResponse>> UpdateQuantityAsync(
            UpdateProductStockQuantityCommand command,
            UpdateProductStockQuantitySpecification specification,
            CancellationToken cancellationToken);
    }
}
