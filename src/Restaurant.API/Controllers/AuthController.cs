using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Auth.Commands.LoginCommand;
using Restaurant.Application.Features.Auth.Commands.RefreshTokenCommand;
using Restaurant.Application.Features.Auth.Commands.RegisterCommand;
using Restaurant.Application.Features.Auth.Commands.VerifyEmailCommand;
using Restaurant.Contract.DTOs.Auth;
using System.Threading.Tasks;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new RegisterCommand(request), cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new LoginCommand(request), cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new RefreshTokenCommand(request), cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string email, [FromQuery] string token, CancellationToken cancellationToken)
        {
            var request = new VerifyEmailRequest(email, token);
            var result = await _mediator.Send(new VerifyEmailCommand(request), cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
