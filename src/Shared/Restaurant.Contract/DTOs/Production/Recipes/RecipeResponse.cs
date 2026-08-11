using Restaurant.Contract.DTOs.Production.RecipeIngredients;

namespace Restaurant.Contract.DTOs.Production.Recipes
{
    public class RecipeResponse
    {
        public string Id { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;
        public string? Instruction { get; set; }

        public IEnumerable<RecipeIngredientResponse> RecipeIngredients { get; set; } = [];
    }
}
