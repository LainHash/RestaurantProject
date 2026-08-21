using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Commerce.Wishlists.Queries.GetBySessionId;

namespace Restaurant.API.Controllers.Guest
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestsController(IMediator mediator) : ControllerBase
    {   
        private readonly IMediator _mediator = mediator;

        [HttpGet("{sessionId}/wishlist")]
        public async Task<IActionResult> GetWishlist(
            [FromRoute] Guid sessionId,
            CancellationToken cancellationToken)
        {
            var query = new GetWishlistBySessionIdQuery(sessionId.ToString());
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
