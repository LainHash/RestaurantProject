namespace Restaurant.Contract.DTOs.Production.Recipes
{
    public class UpdateRecipeRequest
    {
        public string ProductId { get; set; } = string.Empty;
        public string? Instructions { get; set; }
    }
}
