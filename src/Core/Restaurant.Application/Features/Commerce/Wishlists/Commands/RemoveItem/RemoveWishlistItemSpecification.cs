using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Commerce.Wishlists.Commands.RemoveItem
{
    public class RemoveWishlistItemSpecification
        : BaseSpecification<Wishlist>
    {
        public RemoveWishlistItemSpecification(RemoveWishlistItemCommand command)
        {
            AddIncludeAggregator(x => x.Include(w => w.Customer)
                                        .ThenInclude(c => c!.User));
            AddIncludeAggregator(x => x.Include(w => w.WishlistItems)
                                        .ThenInclude(wi => wi.Product));

            AddCriteria(x => x.Customer!.User.PublicId == command.UserId);
        }
    }
}
