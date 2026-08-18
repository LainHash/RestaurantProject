using AutoMapper;
using Restaurant.Application.Features.Guest.Customers.Queries.GetByUserId;
using Restaurant.Application.Services.Guest;
using Restaurant.Contract.DTOs.Guest.Customers;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Guest;
using System.Net;

namespace Restaurant.Persistence.Services.Guest
{
    internal class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        private readonly IMapper _mapper;

        public CustomerService(
            ICustomerRepository customerRepository,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<Result<CustomerResponse>> GetByUserIdAsync(
            GetCustomerByUserIdSpecification specification,
            CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.FindAsync(specification, cancellationToken);
            if(customer is null)
            {
                return Result<CustomerResponse>
                    .Fail(Error<Customer>.NotFound, HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<CustomerResponse>(customer);
            return Result<CustomerResponse>
                .Succeed(response, Success<Customer>.Retrieved);
        }
    }
}
