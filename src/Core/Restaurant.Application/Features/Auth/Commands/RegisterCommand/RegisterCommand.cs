using MediatR;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.RegisterCommand
{
    public record RegisterCommand(RegisterRequest Request) 
        : IRequest<Result<bool>>;

}
