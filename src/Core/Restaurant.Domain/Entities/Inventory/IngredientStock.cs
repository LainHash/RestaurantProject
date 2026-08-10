using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Territory;

namespace Restaurant.Domain.Entities.Inventory
{
    public class IngredientStock : SoftDeletableEntity
    {
        public decimal QuantityOnHand { get; private set; }

        public int IngredientId { get; private set; }
        public int BranchId { get; private set; }

        public Ingredient Ingredient { get; private set; } = null!;
        public Branch Branch { get; private set; } = null!;
    }
}
