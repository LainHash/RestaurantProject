namespace Restaurant.Contract.DTOs.Production.Recipes
{
    public class CreateRecipeRequest
    {
        public string ProductId { get; set; } = string.Empty;
        public string? Instructions { get; set; }
    }
}
