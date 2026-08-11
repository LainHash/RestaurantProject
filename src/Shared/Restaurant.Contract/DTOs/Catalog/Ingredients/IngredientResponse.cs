namespace Restaurant.Contract.DTOs.Catalog.Ingredients
{
    public class IngredientResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public string? BrandName { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public string UnitName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
    }
}
