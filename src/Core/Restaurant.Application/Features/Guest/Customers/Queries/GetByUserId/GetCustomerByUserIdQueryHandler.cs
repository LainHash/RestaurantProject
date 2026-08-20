using MediatR;
using Restaurant.Application.Services.Guest;
using Restaurant.Contract.DTOs.Guest.Customers;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Guest.Customers.Queries.GetByUserId
{
    internal class GetCustomerByUserIdQueryHandler(ICustomerService customerService)
                : IRequestHandler<GetCustomerByUserIdQuery, Result<CustomerResponse>>
    {
        private readonly ICustomerService _customerService = customerService;

        public async Task<Result<CustomerResponse>> Handle(GetCustomerByUserIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetCustomerByUserIdSpecification(request);
            var response = await _customerService.GetByUserIdAsync(specification, cancellationToken);
            return response;
        }
    }
}
