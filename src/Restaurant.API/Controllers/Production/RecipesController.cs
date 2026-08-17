using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Production.Recipes.Commands.AddIngredient;
using Restaurant.Application.Features.Production.Recipes.Commands.Create;
using Restaurant.Application.Features.Production.Recipes.Commands.Update;
using Restaurant.Application.Features.Production.Recipes.Queries.GetAll;
using Restaurant.Application.Features.Production.Recipes.Queries.GetById;
using Restaurant.Contract.DTOs.Production.RecipeIngredients;
using Restaurant.Contract.DTOs.Production.Recipes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Restaurant.API.Controllers.Production
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize(Roles = "SuperAdmin,Admin,Manager,Chef")]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllRecipesQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin,Manager,Chef")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(
            [FromRoute] string id,
            CancellationToken cancellationToken)
        {
            var query = new GetRecipeByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin,Chef")]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateRecipeRequest body,
            CancellationToken cancellationToken)
        {
            var command = new CreateRecipeCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin,Chef")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] string id,
            [FromBody] UpdateRecipeRequest body,
            CancellationToken cancellationToken)
        {
            var command = new UpdateRecipeCommand(id, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin,Chef")]
        [HttpPatch("{id}/ingredients")]
        public async Task<IActionResult> AddIngredient(
            [FromRoute] string id,
            [FromBody] IEnumerable<AddRecipeIngredientRequest> body,
            CancellationToken cancellationToken)
        {
            var command = new AddRecipeIngredientCommand(id, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
