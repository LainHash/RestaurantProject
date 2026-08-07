using MediatR;
using Restaurant.Application.Services.Inventory;
using Restaurant.Contract.DTOs.Inventory.ProductStocks;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.ProductStocks.Commands.UpdateQuantity
{
    internal class UpdateProductStockQuantityCommandHandler(IProductStockService productStockService)
                : IRequestHandler<UpdateProductStockQuantityCommand, Result<ProductStockResponse>>
    {
        private readonly IProductStockService _productStockService = productStockService;

        public async Task<Result<ProductStockResponse>> Handle(UpdateProductStockQuantityCommand request, CancellationToken cancellationToken)
        {
            var specification = new UpdateProductStockQuantitySpecification(request);
            var response = await _productStockService.UpdateQuantityAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
