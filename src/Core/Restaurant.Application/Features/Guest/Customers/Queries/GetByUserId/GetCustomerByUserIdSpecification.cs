using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Guest.Customers.Queries.GetByUserId
{
    public class GetCustomerByUserIdSpecification
        : BaseSpecification<Customer>
    {
        public GetCustomerByUserIdSpecification(GetCustomerByUserIdQuery query)
        {
            AddIncludeAggregator(x => x.Include(c => c.User)
                                        .ThenInclude(u => u.Role));
            AddIncludeAggregator(x => x.Include(c => c.User)
                                        .ThenInclude(u => u.PersonalProfile));
            AddInclude(x => x.Wallet!);
            Criteria = c => c.User.PublicId == query.UserId;
        }
    }
}
