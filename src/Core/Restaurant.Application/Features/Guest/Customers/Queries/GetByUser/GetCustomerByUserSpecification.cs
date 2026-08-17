using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Guest.Customers.Queries.GetByUser
{
    public class GetCustomerByUserSpecification
        : BaseSpecification<Customer>
    {
        public GetCustomerByUserSpecification(GetCustomerByUserQuery query)
        {
            AddIncludeAggregator(x => x.Include(c => c.User)
                                        .ThenInclude(u => u.Role));
            AddIncludeAggregator(x => x.Include(c => c.User)
                                        .ThenInclude(u => u.PersonalProfile));

            Criteria = c => c.User.PublicId == query.UserId;
        }
    }
}
