using MediatR;
using Restaurant.Application.Services.Inventory;
using Restaurant.Contract.DTOs.Inventory.ProductStocks;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.ProductStocks.Queries.GetAllByProductId
{
    internal class GetAllProductStocksByProductIdQueryHandler(IProductStockService productStockService)
                : IRequestHandler<GetAllProductStocksByProductIdQuery, Result<IEnumerable<ProductStockResponse>>>
    {
        private readonly IProductStockService _productStockService = productStockService;

        public async Task<Result<IEnumerable<ProductStockResponse>>> Handle(GetAllProductStocksByProductIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllProductStocksByProductIdSpecification(request);
            var response = await _productStockService.GetAllByProductIdAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
