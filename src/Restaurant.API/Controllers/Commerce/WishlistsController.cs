using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Commerce.Wishlists.Commands.AddItem;
using Restaurant.Application.Features.Commerce.Wishlists.Commands.Merge;
using Restaurant.Application.Features.Commerce.Wishlists.Commands.RemoveItem;
using Restaurant.Application.Features.Commerce.Wishlists.Queries.GetWishlist;
using Restaurant.Contract.DTOs.Commerce.WishlistItems;
using System.Security.Claims;

namespace Restaurant.API.Controllers.Commerce
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetWishlist(CancellationToken cancellationToken)
        {
            var (userId, sessionId) = GetWishlistOwner();

            var query = new GetWishlistQuery(userId, sessionId);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItem(
            [FromBody] AddWishlistItemRequest body,
            CancellationToken cancellationToken)
        {
            var (userId, sessionId) = GetWishlistOwner();

            var command = new AddWishlistItemCommand(userId, sessionId, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpDelete("items")]
        public async Task<IActionResult> RemoveItem(
            [FromBody] RemoveWishlistItemRequest body,
            CancellationToken cancellationToken)
        {
            var (userId, sessionId) = GetWishlistOwner();

            var command = new RemoveWishlistItemCommand(userId, sessionId, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost("items/merge")]
        public async Task<IActionResult> Merge(CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var sessionId = Request.Headers["X-Session-Id"].FirstOrDefault();

            var command = new MergeWishlistCommand(userId!, sessionId!);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        private (string? UserId, string? SessionId) GetWishlistOwner()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return (User.FindFirst(ClaimTypes.NameIdentifier)?.Value, null);
            }

            return (null, Request.Headers["X-Session-Id"].FirstOrDefault());
        }
    }
}
