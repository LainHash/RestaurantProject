using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Entities.Production
{
    public class Recipe : SoftDeletableEntity
    {
        public int ProductId { get; private set; }

        public string? Instructions { get; private set; }

        public Product Product { get; private set; } = null!;
        public ICollection<RecipeIngredient> RecipeIngredients { get; private set; } = [];
    }
}
