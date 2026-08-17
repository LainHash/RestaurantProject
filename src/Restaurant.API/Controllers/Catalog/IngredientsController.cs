using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Catalog.Ingredients.Commands.Create;
using Restaurant.Application.Features.Catalog.Ingredients.Commands.Delete;
using Restaurant.Application.Features.Catalog.Ingredients.Commands.Restore;
using Restaurant.Application.Features.Catalog.Ingredients.Commands.Update;
using Restaurant.Application.Features.Catalog.Ingredients.Queries.GetAll;
using Restaurant.Application.Features.Catalog.Ingredients.Queries.GetById;
using Restaurant.Application.Features.Inventory.IngredientStocks.Commands.UpdateQuantity;
using Restaurant.Application.Features.Inventory.IngredientStocks.Queries.GetAllByIngredientId;
using Restaurant.Contract.DTOs.Catalog.Ingredients;
using Restaurant.Contract.DTOs.Inventory.IngredientStocks;

namespace Restaurant.API.Controllers.Catalog
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize(Roles = "SuperAdmin,Admin,Manager,Chef,InventoryManager")]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllIngredientsQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin,Manager,Chef,InventoryManager")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] string id,
            CancellationToken cancellationToken)
        {
            var query = new GetIngredientByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin,InventoryManager")]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateIngredientRequest body,
            CancellationToken cancellationToken)
        {
            var command = new CreateIngredientCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin,InventoryManager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] string id,
            [FromBody] UpdateIngredientRequest body,
            CancellationToken cancellationToken)
        {
            var command = new UpdateIngredientCommand(id, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] string id,
            CancellationToken cancellationToken)
        {
            var command = new DeleteIngredientCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore(
            [FromRoute] string id,
            CancellationToken cancellationToken)
        {
            var command = new RestoreIngredientCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin,Manager,InventoryManager")]
        [HttpGet("{id}/stock-list")]
        public async Task<IActionResult> GetAllStock(
            [FromRoute] string id,
            CancellationToken cancellationToken)
        {
            var query = new GetAllIngredientStocksByIngredientIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin,Manager,InventoryManager")]
        [HttpPatch("{ingredientId}/branch/{branchId}/update-quantity")]
        public async Task<IActionResult> UpdateQuantity(
            [FromRoute] string ingredientId,
            [FromRoute] string branchId,
            [FromBody] UpdateIngredientStockQuantityRequest body,
            CancellationToken cancellationToken)
        {
            var command = new UpdateIngredientStockQuantityCommand(ingredientId, branchId, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
