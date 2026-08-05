using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Catalog.Categories.Commands.Create;
using Restaurant.Application.Features.Catalog.Categories.Commands.Delete;
using Restaurant.Application.Features.Catalog.Categories.Commands.Restore;
using Restaurant.Application.Features.Catalog.Categories.Commands.Update;
using Restaurant.Application.Features.Catalog.Categories.Queries.GetAll;
using Restaurant.Application.Features.Catalog.Categories.Queries.GetById;
using Restaurant.Application.Features.Catalog.Categories.Queries.GetByName;
using Restaurant.Contract.DTOs.Catalog.Categories;

namespace Restaurant.API.Controllers.Catalog
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllCategoriesQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] string id,
            CancellationToken cancellationToken)
        {
            var query = new GetCategoryByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetByName(
            [FromRoute] string name,
            CancellationToken cancellationToken)
        {
            var query = new GetCategoryByNameQuery(name);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateCategoryRequest body,
            CancellationToken cancellationToken)
        {
            var command = new CreateCategoryCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] string id,
            [FromBody] UpdateCategoryRequest body,
            CancellationToken cancellationToken)
        {
            var command = new UpdateCategoryCommand(id, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] string id,
            CancellationToken cancellationToken)
        {
            var command = new DeleteCategoryCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore(
            [FromRoute] string id,
            CancellationToken cancellationToken)
        {
            var command = new RestoreCategoryCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
