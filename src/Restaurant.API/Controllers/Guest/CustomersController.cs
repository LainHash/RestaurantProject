using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Guest.Customers.Queries.GetByUser;
using System.Security.Claims;

namespace Restaurant.API.Controllers.Guest
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetOne(CancellationToken cancellationToken)
        {
            string userId = null!;

            if (User.Identity?.IsAuthenticated == true)
            {
                userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            }

            var query = new GetCustomerByUserQuery(userId);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
