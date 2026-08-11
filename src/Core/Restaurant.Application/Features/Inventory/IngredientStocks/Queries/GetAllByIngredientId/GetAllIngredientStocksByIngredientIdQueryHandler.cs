using MediatR;
using Restaurant.Application.Services.Inventory;
using Restaurant.Contract.DTOs.Inventory.IngredientStocks;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.IngredientStocks.Queries.GetAllByIngredientId
{
    internal class GetAllIngredientStocksByIngredientIdQueryHandler(IIngredientStockService ingredientStockService)
                : IRequestHandler<GetAllIngredientStocksByIngredientIdQuery, Result<IEnumerable<IngredientStockResponse>>>
    {
        private readonly IIngredientStockService _ingredientStockService = ingredientStockService;

        public async Task<Result<IEnumerable<IngredientStockResponse>>> Handle(GetAllIngredientStocksByIngredientIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllIngredientStocksByIngredientIdSpecification(request);
            var response = await _ingredientStockService.GetAllByIngredientIdAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
