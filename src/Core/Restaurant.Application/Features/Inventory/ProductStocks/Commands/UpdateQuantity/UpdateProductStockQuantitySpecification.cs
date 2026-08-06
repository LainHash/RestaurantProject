using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Specifications;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Restaurant.Application.Features.Inventory.ProductStocks.Commands.UpdateQuantity
{
    public class UpdateProductStockQuantitySpecification
        : BaseSpecification<ProductStock>
    {
        public UpdateProductStockQuantitySpecification(UpdateProductStockQuantityCommand command)
        {
            EnableSoftDeleteFilter();

            AddInclude(x => x.Product);
            AddInclude(x => x.Branch);

            Criteria = ps => string.Equals(ps.Product.PublicId, command.ProductId)
                            && string.Equals(ps.Branch.PublicId, command.BranchId);
        }
    }
}
