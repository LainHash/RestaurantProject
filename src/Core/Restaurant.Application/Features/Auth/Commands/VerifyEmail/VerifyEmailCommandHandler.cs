using MediatR;
using Restaurant.Application.Services.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.VerifyEmail
{
    internal class VerifyEmailCommandHandler(IAuthenticationService authenticationService)
                : IRequestHandler<VerifyEmailCommand, Result>
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;

        public async Task<Result> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await _authenticationService.VerifyEmailAsync(request, cancellationToken);
            return response;
        }
    }
}
