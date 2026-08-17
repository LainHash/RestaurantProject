using MediatR;
using Restaurant.Application.Services.Guest;
using Restaurant.Contract.DTOs.Guest.Customers;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Guest.Customers.Queries.GetByUser
{
    internal class GetCustomerByUserQueryHandler(ICustomerService customerService)
                : IRequestHandler<GetCustomerByUserQuery, Result<CustomerResponse>>
    {
        private readonly ICustomerService _customerService = customerService;

        public async Task<Result<CustomerResponse>> Handle(GetCustomerByUserQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetCustomerByUserSpecification(request);
            var response = await _customerService.GetByUserAsync(specification, cancellationToken);
            return response;
        }
    }
}
