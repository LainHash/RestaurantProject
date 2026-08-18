using Restaurant.Application.Features.Guest.Customers.Queries.GetByUserId;
using Restaurant.Contract.DTOs.Guest.Customers;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Guest
{
    public interface ICustomerService
    {
        Task<Result<CustomerResponse>> GetByUserIdAsync(
            GetCustomerByUserIdSpecification specification,
            CancellationToken cancellationToken);
    }
}
