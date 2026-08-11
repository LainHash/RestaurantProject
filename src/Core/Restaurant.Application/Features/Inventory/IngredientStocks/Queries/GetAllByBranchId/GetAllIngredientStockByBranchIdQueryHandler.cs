using MediatR;
using Restaurant.Application.Services.Inventory;
using Restaurant.Contract.DTOs.Inventory.IngredientStocks;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.IngredientStocks.Queries.GetAllByBranchId
{
    internal class GetAllIngredientStockByBranchIdQueryHandler(IIngredientStockService ingredientStockService)
                : IRequestHandler<GetAllIngredientStockByBranchIdQuery, Result<IEnumerable<IngredientStockResponse>>>
    {
        private readonly IIngredientStockService _ingredientStockService = ingredientStockService;

        public async Task<Result<IEnumerable<IngredientStockResponse>>> Handle(GetAllIngredientStockByBranchIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllIngredientStockByBranchIdSpecification(request);
            var response = await _ingredientStockService.GetAllByBranchIdAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
