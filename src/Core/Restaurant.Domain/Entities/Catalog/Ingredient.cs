using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Entities.Pricing;
using Restaurant.Domain.Entities.Production;

namespace Restaurant.Domain.Entities.Catalog
{
    public partial class Ingredient : SoftDeletableEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }

        public int BaseUnitId { get; private set; }
        public int CategoryId { get; private set; }
        public int? BrandId { get; private set; }

        public Unit BaseUnit { get; private set; } = null!;
        public Brand? Brand { get; private set; }
        public IngredientCategory IngredientCategory { get; private set; } = null!;
        public IngredientPrice IngredientPrice { get; private set; } = null!;
        public ICollection<IngredientStock> IngredientStocks { get; private set; } = [];

        public ICollection<RecipeIngredient> RecipeIngredients { get; private set; } = [];
    }

    public partial class Ingredient
    {
        public Ingredient SetBrand(int? brandId)
        {
            BrandId = brandId;
            return this;
        }

        public Ingredient SetCategory(int categoryId)
        {
            CategoryId = categoryId;
            return this;
        }

        public Ingredient SetUnit(int unitId)
        {
            BaseUnitId = unitId;
            return this;
        }
    }
}
