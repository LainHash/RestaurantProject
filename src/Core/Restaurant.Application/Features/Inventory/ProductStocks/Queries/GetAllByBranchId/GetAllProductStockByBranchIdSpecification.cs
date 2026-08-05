using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Inventory.ProductStocks.Queries.GetAllByBranchId
{
    public class GetAllProductStockByBranchIdSpecification
        : BaseSpecification<ProductStock>
    {
        public GetAllProductStockByBranchIdSpecification(GetAllProductStockByBranchIdQuery query)
        {
            EnableSoftDeleteFilter();

            AddInclude(x => x.Product);
            AddInclude(x => x.Branch);

            Criteria = ps => string.Equals(ps.Branch.PublicId, query.BranchId);
        }
    }
}
