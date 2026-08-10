namespace Restaurant.Contract.DTOs.Catalog.IngredientCategories
{
    public class IngredientCategoryResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
