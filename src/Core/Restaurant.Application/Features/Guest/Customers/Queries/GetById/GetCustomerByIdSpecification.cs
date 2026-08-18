using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Guest.Customers.Queries.GetById
{
    public class GetCustomerByIdSpecification
        : BaseSpecification<Customer>
    {
        public GetCustomerByIdSpecification(GetCustomerByIdQuery query)
        {
            AddIncludeAggregator(x => x.Include(c => c.User)
                                        .ThenInclude(u => u.Role));
            AddIncludeAggregator(x => x.Include(c => c.User)
                                        .ThenInclude(u => u.PersonalProfile));

            Criteria = c => c.PublicId == query.Id;
        }
    }
}
