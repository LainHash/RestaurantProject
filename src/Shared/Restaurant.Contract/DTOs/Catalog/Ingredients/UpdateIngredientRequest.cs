namespace Restaurant.Contract.DTOs.Catalog.Ingredients
{
    public class UpdateIngredientRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public string? BrandId { get; set; }
        public string CategoryId { get; set; } = string.Empty;
        public string UnitId { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }
    }
}
