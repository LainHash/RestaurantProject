using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Territory;

namespace Restaurant.Domain.Entities.Inventory
{
    public partial class ProductStock : SoftDeletableEntity
    {
        public string Unit { get; private set; } = string.Empty;
        public decimal QuantityOnHand { get; private set; }

        public int ProductId { get; private set; }
        public int BranchId { get; private set; }

        public Product Product { get; private set; } = null!;
        public Branch Branch { get; private set; } = null!;
    }

    public partial class ProductStock
    {
        public ProductStock() { }

        public ProductStock(int productId, int branchId)
        {
            ProductId = productId;
            BranchId = branchId;
        }

        public void SetProductId(int productId)
        {
            ProductId = productId;
        }

        public void SetBranchId(int branchId)
        {
            BranchId = branchId;
        }

        public ProductStock(string unit, decimal quantityOnHand, int productId, int branchId)
        {
            Unit = unit;
            QuantityOnHand = quantityOnHand;
            ProductId = productId;
            BranchId = branchId;
        }
        public void UpdateQuantity(decimal newQuantity)
        {
            QuantityOnHand = newQuantity;
        }
    }
}
