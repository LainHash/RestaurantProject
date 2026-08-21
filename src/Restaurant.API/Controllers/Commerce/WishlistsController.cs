using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Commerce.Wishlists.Commands.AddItem;
using Restaurant.Contract.DTOs.Commerce.WishlistItems;

namespace Restaurant.API.Controllers.Commerce
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("wishlist/{id}")]
        public async Task<IActionResult> AddItem(
            [FromRoute] string id,
            [FromBody] AddWishlistItemRequest body,
            CancellationToken cancellationToken)
        {
            var command = new AddWishlistItemCommand(id, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
