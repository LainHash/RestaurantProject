using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Commerce.Carts.Commands.AddItem;
using Restaurant.Application.Features.Commerce.Carts.Commands.RemoveItem;
using Restaurant.Application.Features.Commerce.Carts.Queries.GetCart;
using Restaurant.Contract.DTOs.Commerce.CartItems;
using System.Security.Claims;

namespace Restaurant.API.Controllers.Commerce
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetCart(CancellationToken cancellationToken)
        {
            var (userId, sessionId) = GetCartOwner();

            var query = new GetCartQuery(userId, sessionId);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItem(
            [FromBody] AddCartItemRequest body,
            CancellationToken cancellationToken)
        {
            var (userId, sessionId) = GetCartOwner();

            var command = new AddCartItemCommand(userId, sessionId, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpDelete("items")]
        public async Task<IActionResult> RemoveItem(
            [FromBody] RemoveCartItemRequest body,
            CancellationToken cancellationToken)
        {
            var (userId, sessionId) = GetCartOwner();

            var command = new RemoveCartItemCommand(userId, sessionId, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        private (string? UserId, string? SessionId) GetCartOwner()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return (
                    User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    null
                );
            }

            return (
                null,
                Request.Headers["X-Session-Id"].FirstOrDefault()
            );
        }
    }
}
