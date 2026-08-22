using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Commerce.Wishlists.Commands.Merge
{
    public class MergeWishlistSpecification
        : BaseSpecification<Wishlist>
    {
        public MergeWishlistSpecification(MergeWishlistCommand command)
        {
            AddIncludeAggregator(x => x.Include(w => w.Customer!)
                                            .ThenInclude(c => c!.User));
            AddIncludeAggregator(x => x.Include(w => w.WishlistItems)
                                        .ThenInclude(wi => wi.Product));

            AddCriteria(x => x.Customer!.User.PublicId == command.UserId);
        }
    }
}
