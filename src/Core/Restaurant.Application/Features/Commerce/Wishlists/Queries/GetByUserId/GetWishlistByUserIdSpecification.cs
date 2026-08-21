using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Commerce.Wishlists.Queries.GetByUserId
{
    public class GetWishlistByUserIdSpecification
        : BaseSpecification<Wishlist>
    {
        public GetWishlistByUserIdSpecification(GetWishlistByUserIdQuery query)
        {
            AddIncludeAggregator(x => x.Include(w => w.Customer)
                                        .ThenInclude(c => c!.User));
            AddIncludeAggregator(x => x.Include(w => w.WishlistItems)
                                        .ThenInclude(wi => wi.Product));


            AddCriteria(x => x.Customer!.User.PublicId == query.UserId);
        }
    }
}
