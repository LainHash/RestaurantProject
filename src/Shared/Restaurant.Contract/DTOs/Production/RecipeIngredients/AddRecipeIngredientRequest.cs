namespace Restaurant.Contract.DTOs.Production.RecipeIngredients
{
    public class AddRecipeIngredientRequest
    {
        public string IngredientId { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string UnitId { get; set; } = string.Empty;
    }
}
