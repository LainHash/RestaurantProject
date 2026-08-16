using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Auth.Commands.CompleteProfile;
using Restaurant.Application.Features.Auth.Commands.Login;
using Restaurant.Application.Features.Auth.Commands.Register;
using Restaurant.Application.Features.Auth.Commands.ResendVerification;
using Restaurant.Application.Features.Auth.Commands.VerifyEmail;
using Restaurant.Contract.DTOs.Auth;

namespace Restaurant.API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequest body,
            CancellationToken cancellationToken)
        {
            var command = new LoginCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterRequest body,
            CancellationToken cancellationToken)
        {
            var command = new RegisterCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(
            [FromBody] VerifyEmailRequest body,
            CancellationToken cancellationToken)
        {
            var command = new VerifyEmailCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost("resend-verification")]
        public async Task<IActionResult> ResendVerification(
            [FromBody] ResendVerificationRequest body,
            CancellationToken cancellationToken)
        {
            var command = new ResendVerificationCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost("complete-profile")]
        public async Task<IActionResult> CompleteProfile(
            [FromBody] CompleteProfileRequest body,
            CancellationToken cancellationToken)
        {
            var command = new CompleteProfileCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
