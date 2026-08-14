using MediatR;
using Restaurant.Application.Services.Auth;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.RefreshTokenCommand
{
    public class RefreshTokenCommandHandler(IAuthService authService) 
        : IRequestHandler<RefreshTokenCommand, Result<AuthResponse>>
    {
        private readonly IAuthService _authService = authService;

        public Task<Result<AuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return _authService.RefreshTokenAsync(request.Request, cancellationToken);
        }
    }
}
