using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Inventory.ProductStocks.Queries.GetAllByProductId
{
    public class GetAllProductStocksByProductIdSpecification
        : BaseSpecification<ProductStock>
    {
        public GetAllProductStocksByProductIdSpecification(GetAllProductStocksByProductIdQuery query)
        {
            EnableSoftDeleteFilter();

            AddInclude(x => x.Product);
            AddInclude(x => x.Branch);

            Criteria = ps => string.Equals(ps.Product.PublicId, query.ProductId);
        }
    }
}
