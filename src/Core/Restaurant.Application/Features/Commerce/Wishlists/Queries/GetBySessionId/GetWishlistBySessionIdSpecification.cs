using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Commerce.Wishlists.Queries.GetBySessionId
{
    public class GetWishlistBySessionIdSpecification
        : BaseSpecification<Wishlist>
    {
        public GetWishlistBySessionIdSpecification(GetWishlistBySessionIdQuery query)
        {
            AddIncludeAggregator(x => x.Include(w => w.WishlistItems)
                                        .ThenInclude(wi => wi.Product));

            AddCriteria(x => x.SessionId == query.SessionId);
        }
    }
}
