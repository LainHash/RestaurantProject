using MediatR;
using Restaurant.Contract.DTOs.Guest.Customers;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Guest.Customers.Queries.GetAll
{
    public record GetAllCustomersQuery()
        : IRequest<Result<IEnumerable<CustomerResponse>>>
    {
    }
}
