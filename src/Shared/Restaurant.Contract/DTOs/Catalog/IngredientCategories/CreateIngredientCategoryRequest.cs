namespace Restaurant.Contract.DTOs.Catalog.IngredientCategories
{
    public class CreateIngredientCategoryRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
