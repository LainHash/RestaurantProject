using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Inventory;

namespace Restaurant.Domain.Entities.Production
{
    public partial class RecipeIngredient : SoftDeletableEntity
    {
        public int RecipeId { get; private set; }
        public int IngredientId { get; private set; }

        public decimal Quantity { get; private set; }

        public int UnitId { get; private set; }

        public Recipe Recipe { get; private set; } = null!;
        public Ingredient Ingredient { get; private set; } = null!;
        public Unit Unit { get; private set; } = null!;
    }

    public partial class RecipeIngredient
    {
        public RecipeIngredient SetIngredient(int ingredientId)
        {
            IngredientId = ingredientId;
            return this;
        }

        public RecipeIngredient SetRecipe(int recipeId)
        {
            RecipeId = recipeId;
            return this;
        }

        public RecipeIngredient SetUnit(int unitId)
        {
            UnitId = unitId;
            return this;
        }
    }
}
