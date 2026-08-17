using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Guest.Customers.Queries.GetByUser;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Restaurant.API.Controllers.Guest
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer,SuperAdmin")]
    public class CustomersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetOne(CancellationToken cancellationToken)
        {
            string? userId = null!;

            if (User.Identity?.IsAuthenticated == true)
            {
                userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                                   ?? User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            }

            var query = new GetCustomerByUserQuery(userId!);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
