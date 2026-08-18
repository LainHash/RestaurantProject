using MediatR;
using Restaurant.Application.Services.Guest;
using Restaurant.Contract.DTOs.Guest.Customers;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Guest.Customers.Queries.GetAll
{
    internal class GetAllCustomersQueryHandler(ICustomerService customerService)
                : IRequestHandler<GetAllCustomersQuery, Result<IEnumerable<CustomerResponse>>>
    {
        private readonly ICustomerService _customerService = customerService;

        public async Task<Result<IEnumerable<CustomerResponse>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllCustomersSpecification(request);
            var response = await _customerService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}
