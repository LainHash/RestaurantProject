using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Inventory.ProductStocks.Queries.GetAllByBranchId;
using Restaurant.Application.Features.Territory.Branches.Queries.GetAll;

namespace Restaurant.API.Controllers.Territory
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllBranchesQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpGet("{id}/stock-list")]
        public async Task<IActionResult> GetAllStock(
            [FromRoute] string id,
            CancellationToken cancellationToken)
        {
            var query = new GetAllProductStockByBranchIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
