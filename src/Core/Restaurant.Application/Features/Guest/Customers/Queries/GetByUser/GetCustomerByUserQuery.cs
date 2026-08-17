using MediatR;
using Restaurant.Contract.DTOs.Guest.Customers;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Guest.Customers.Queries.GetByUser
{
    public record GetCustomerByUserQuery(string UserId)
        : IRequest<Result<CustomerResponse>>
    {
    }
}
