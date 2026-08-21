using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Specifications;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Restaurant.Application.Features.Commerce.Wishlists.Commands.AddItem
{
    public class AddWishlistItemSpecification
        : BaseSpecification<Wishlist>
    {
        public AddWishlistItemSpecification(AddWishlistItemCommand command)
        {
            AddIncludeAggregator(x => x.Include(w => w.Customer)
                                        .ThenInclude(c => c!.User));
            AddIncludeAggregator(x => x.Include(w => w.WishlistItems)
                                        .ThenInclude(wi => wi.Product));


            AddCriteria(x => x.Customer!.User.PublicId == command.UserId);
        }
    }
}
