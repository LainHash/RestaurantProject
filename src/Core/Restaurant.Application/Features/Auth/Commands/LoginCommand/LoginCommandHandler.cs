using MediatR;
using Restaurant.Application.Services.Auth;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.LoginCommand
{
    public class LoginCommandHandler(IAuthService authService) 
        : IRequestHandler<LoginCommand, Result<AuthResponse>>
    {
        private readonly IAuthService _authService = authService;

        public Task<Result<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return _authService.LoginAsync(request.Request, cancellationToken);
        }
    }
}
