using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Commerce.Carts.Commands.Merge
{
    public class MergeCartSpecification
        : BaseSpecification<Cart>
    {
        public MergeCartSpecification(MergeCartCommand command)
        {
            AddIncludeAggregator(x => x.Include(w => w.Customer!)
                                            .ThenInclude(c => c!.User));
            AddIncludeAggregator(x => x.Include(w => w.CartItems)
                                        .ThenInclude(wi => wi.Product));

            AddCriteria(x => x.Customer!.User.PublicId == command.UserId);
        }
    }
}
