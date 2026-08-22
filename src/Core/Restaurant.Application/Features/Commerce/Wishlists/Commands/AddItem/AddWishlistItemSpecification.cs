using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Commerce.Wishlists.Commands.AddItem
{
    public class AddWishlistItemSpecification
        : BaseSpecification<Wishlist>
    {
        public AddWishlistItemSpecification(AddWishlistItemCommand command)
        {
            if (command.UserId != null)
            {
                AddIncludeAggregator(x => x.Include(w => w.Customer!)
                                            .ThenInclude(c => c!.User));
                AddIncludeAggregator(x => x.Include(w => w.WishlistItems)
                                            .ThenInclude(wi => wi.Product));

                AddCriteria(x => x.Customer!.User.PublicId == command.UserId);
            }
            else
            {
                AddIncludeAggregator(x => x.Include(w => w.WishlistItems)
                                            .ThenInclude(wi => wi.Product));

                AddCriteria(x => x.SessionId == command.SessionId);
            }
        }
    }
}
