namespace Restaurant.Contract.DTOs.Catalog.Categories
{
    public class CreateProductCategoryRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
