namespace Restaurant.Contract.DTOs.Production.RecipeIngredients
{
    public class RecipeIngredientResponse
    {
        public string IngredientName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string UnitName { get; set; } = string.Empty;
    }
}
