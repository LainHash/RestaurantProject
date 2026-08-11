using MediatR;
using Restaurant.Contract.DTOs.Inventory.IngredientStocks;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.IngredientStocks.Queries.GetAllByIngredientId
{
    public record GetAllIngredientStocksByIngredientIdQuery(string IngredientId)
        : IRequest<Result<IEnumerable<IngredientStockResponse>>>
    {
    }
}
