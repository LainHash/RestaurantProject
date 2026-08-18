using MediatR;
using Restaurant.Contract.DTOs.Guest.Customers;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Guest.Customers.Queries.GetById
{
    public record GetCustomerByIdQuery(string Id)
        : IRequest<Result<CustomerResponse>>
    {
    }
}
