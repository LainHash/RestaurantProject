using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Production.Recipes.Queries.GetAll;
using Restaurant.Application.Features.Production.Recipes.Queries.GetById;

namespace Restaurant.API.Controllers.Production
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllRecipesQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneById(
            [FromRoute] string id,
            CancellationToken cancellationToken)
        {
            var query = new GetRecipeByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
