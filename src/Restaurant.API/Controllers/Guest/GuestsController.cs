using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.API.Controllers.Guest
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestsController(IMediator mediator) : ControllerBase
    {   
        private readonly IMediator _mediator = mediator;
    }
}
