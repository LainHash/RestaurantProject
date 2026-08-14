using MediatR;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.LoginCommand
{
    public record LoginCommand(LoginRequest Request) 
        : IRequest<Result<AuthResponse>>;

}
