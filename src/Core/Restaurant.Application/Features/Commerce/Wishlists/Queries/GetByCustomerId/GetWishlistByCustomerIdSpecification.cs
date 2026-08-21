using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Commerce.Wishlists.Queries.GetByCustomerId
{
    public class GetWishlistByCustomerIdSpecification
        : BaseSpecification<Wishlist>
    {
        public GetWishlistByCustomerIdSpecification(GetWishlistByCustomerIdQuery query)
        {
            AddInclude(x => x.Customer!);
            AddIncludeAggregator(x => x.Include(w => w.WishlistItems)
                                        .ThenInclude(wi => wi.Product));


            AddCriteria(x => x.Customer!.PublicId == query.CustomerId);
        }
    }
}
