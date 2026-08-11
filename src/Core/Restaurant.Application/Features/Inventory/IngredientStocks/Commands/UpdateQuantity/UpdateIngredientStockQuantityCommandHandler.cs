using MediatR;
using Restaurant.Application.Services.Inventory;
using Restaurant.Contract.DTOs.Inventory.IngredientStocks;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.IngredientStocks.Commands.UpdateQuantity
{
    internal class UpdateIngredientStockQuantityCommandHandler(IIngredientStockService ingredientStockService)
                : IRequestHandler<UpdateIngredientStockQuantityCommand, Result<IngredientStockResponse>>
    {
        private readonly IIngredientStockService _ingredientStockService = ingredientStockService;

        public async Task<Result<IngredientStockResponse>> Handle(UpdateIngredientStockQuantityCommand request, CancellationToken cancellationToken)
        {
            var specification = new UpdateIngredientStockQuantitySpecification(request);
            var response = await _ingredientStockService.UpdateQuantityAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
