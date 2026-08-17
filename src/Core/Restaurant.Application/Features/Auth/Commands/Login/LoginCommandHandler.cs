using ConvenienceStore.Contract.DTOs.Authentication;
using MediatR;
using Restaurant.Application.Services.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.Login
{
    internal class LoginCommandHandler(IAuthenticationService authenticationService)
                : IRequestHandler<LoginCommand, Result<AuthenticationResponse>>
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;

        public async Task<Result<AuthenticationResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = await _authenticationService.LoginAsync(request, cancellationToken);
            return response;
        }
    }
}
