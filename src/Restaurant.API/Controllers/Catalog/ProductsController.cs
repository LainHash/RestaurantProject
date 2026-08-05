using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Catalog.Products.Commands.Create;
using Restaurant.Application.Features.Catalog.Products.Queries.GetAll;
using Restaurant.Application.Features.Catalog.Products.Queries.GetById;
using Restaurant.Contract.DTOs.Catalog.Products;

namespace Restaurant.API.Controllers.Catalog
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllProductsQuery query,
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
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateProductRequest body,
            CancellationToken cancellationToken)
        {
            var command = new CreateProductCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
