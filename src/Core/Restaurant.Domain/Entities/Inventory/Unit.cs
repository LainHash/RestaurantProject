using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Enums;

namespace Restaurant.Domain.Entities.Inventory
{
    public class Unit : SoftDeletableEntity
    {
        public string Name { get; private set; } = null!;
        public string Symbol { get; private set; } = null!;

        public UnitType Type { get; private set; }

        public decimal ConversionRate { get; private set; }

        public ICollection<Product> Products { get; private set; } = [];
    }
}
