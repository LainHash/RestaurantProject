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
    }
}
