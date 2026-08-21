using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Commerce.Wishlists.Queries.GetWishlist
{
    public class GetWishlistSpecification
        : BaseSpecification<Wishlist>
    {
        public GetWishlistSpecification(GetWishlistQuery query)
        {
            if (query.CustomerId != null)
            {
                AddIncludeAggregator(x => x.Include(w => w.Customer!)
                                            .ThenInclude(c => c!.User));
                AddIncludeAggregator(x => x.Include(w => w.WishlistItems)
                                            .ThenInclude(wi => wi.Product));

                AddCriteria(x => x.Customer!.PublicId == query.CustomerId);
            }
            else
            {
                AddIncludeAggregator(x => x.Include(w => w.WishlistItems)
                                            .ThenInclude(wi => wi.Product));

                AddCriteria(x => x.SessionId == query.SessionId);
            }
        }
    }
}
