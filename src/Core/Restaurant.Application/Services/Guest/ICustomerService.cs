using Restaurant.Application.Features.Guest.Customers.Queries.GetByUser;
using Restaurant.Contract.DTOs.Guest.Customers;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Guest
{
    public interface ICustomerService
    {
        Task<Result<CustomerResponse>> GetByUserAsync(
            GetCustomerByUserSpecification specification,
            CancellationToken cancellationToken);
    }
}
