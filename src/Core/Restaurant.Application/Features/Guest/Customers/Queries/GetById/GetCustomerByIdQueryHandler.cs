using MediatR;
using Restaurant.Application.Services.Guest;
using Restaurant.Contract.DTOs.Guest.Customers;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Guest.Customers.Queries.GetById
{
    internal class GetCustomerByIdQueryHandler(ICustomerService customerService)
                : IRequestHandler<GetCustomerByIdQuery, Result<CustomerResponse>>
    {
        private readonly ICustomerService _customerService = customerService;

        public async Task<Result<CustomerResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetCustomerByIdSpecification(request);
            var response = await _customerService.GetByIdAsync(specification, cancellationToken);
            return response;
        }
    }
}
