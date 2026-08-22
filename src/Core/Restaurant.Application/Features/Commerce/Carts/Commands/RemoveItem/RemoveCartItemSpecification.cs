using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Commerce.Carts.Commands.RemoveItem
{
    public class RemoveCartItemSpecification
        : BaseSpecification<Cart>
    {
        public RemoveCartItemSpecification(RemoveCartItemCommand command)
        {
            if (command.UserId != null)
            {
                AddIncludeAggregator(x => x.Include(w => w.Customer!)
                                            .ThenInclude(c => c!.User));
                AddIncludeAggregator(x => x.Include(w => w.CartItems)
                                            .ThenInclude(wi => wi.Product));

                AddCriteria(x => x.Customer!.User.PublicId == command.UserId);
            }
            else
            {
                AddIncludeAggregator(x => x.Include(w => w.CartItems)
                                            .ThenInclude(wi => wi.Product));

                AddCriteria(x => x.SessionId == command.SessionId);
            }
        }
    }
}
