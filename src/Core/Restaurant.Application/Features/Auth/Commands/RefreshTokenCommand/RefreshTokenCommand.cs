using MediatR;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.RefreshTokenCommand
{
    public record RefreshTokenCommand(RefreshTokenRequest Request) 
        : IRequest<Result<AuthResponse>>;
}
