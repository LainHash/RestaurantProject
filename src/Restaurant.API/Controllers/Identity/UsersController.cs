using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Identity.OtpVerifications.Commands.ResendVerification;
using Restaurant.Application.Features.Identity.OtpVerifications.Commands.VerifyEmail;
using Restaurant.Application.Features.Identity.PersonalProfiles.Commands.CompleteProfile;
using Restaurant.Contract.DTOs.Auth;

namespace Restaurant.API.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [AllowAnonymous]
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(
            [FromBody] VerifyEmailRequest body,
            CancellationToken cancellationToken)
        {
            var command = new VerifyEmailCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [AllowAnonymous]
        [HttpPost("resend-verification")]
        public async Task<IActionResult> ResendVerification(
            [FromBody] ResendVerificationRequest body,
            CancellationToken cancellationToken)
        {
            var command = new ResendVerificationCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [AllowAnonymous]
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
