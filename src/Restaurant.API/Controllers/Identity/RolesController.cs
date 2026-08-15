using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Identity.Roles.Commands.Create;
using Restaurant.Application.Features.Identity.Roles.Commands.Delete;
using Restaurant.Application.Features.Identity.Roles.Commands.Restore;
using Restaurant.Application.Features.Identity.Roles.Commands.Update;
using Restaurant.Application.Features.Identity.Roles.Queries.GetAll;
using Restaurant.Application.Features.Identity.Roles.Queries.GetById;
using Restaurant.Contract.DTOs.Identity.Roles;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Restaurant.API.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllRolesQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(
            [FromRoute] string id,
            CancellationToken cancellationToken)
        {
            var query = new GetRoleByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateRoleRequest body,
            CancellationToken cancellationToken)
        {
            var command = new CreateRoleCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] string id,
            [FromBody] UpdateRoleRequest body,
            CancellationToken cancellationToken)
        {
            var command = new UpdateRoleCommand(id, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] string id,
            CancellationToken cancellationToken)
        {
            var command = new DeleteRoleCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore(
            [FromRoute] string id,
            CancellationToken cancellationToken)
        {
            var command = new RestoreRoleCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
