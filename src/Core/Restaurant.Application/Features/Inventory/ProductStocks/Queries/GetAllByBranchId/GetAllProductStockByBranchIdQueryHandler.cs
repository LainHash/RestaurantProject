using MediatR;
using Restaurant.Application.Services.Inventory;
using Restaurant.Contract.DTOs.Inventory.ProductStocks;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.ProductStocks.Queries.GetAllByBranchId
{
    internal class GetAllProductStockByBranchIdQueryHandler(IProductStockService productStockService)
                : IRequestHandler<GetAllProductStockByBranchIdQuery, Result<IEnumerable<ProductStockResponse>>>
    {
        private readonly IProductStockService _productStockService = productStockService;

        public async Task<Result<IEnumerable<ProductStockResponse>>> Handle(GetAllProductStockByBranchIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllProductStockByBranchIdSpecification(request);
            var response = await _productStockService.GetAllByBranchIdAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
