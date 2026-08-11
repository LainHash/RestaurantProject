using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Entities.Production
{
    public partial class Recipe : SoftDeletableEntity
    {
        public int ProductId { get; private set; }

        public string? Instructions { get; private set; }

        public Product Product { get; private set; } = null!;
        public ICollection<RecipeIngredient> RecipeIngredients { get; private set; } = [];
    }

    public partial class Recipe
    {
        public Recipe SetProduct(int productId)
        {
            ProductId = productId;
            return this;
        }
    }
}
