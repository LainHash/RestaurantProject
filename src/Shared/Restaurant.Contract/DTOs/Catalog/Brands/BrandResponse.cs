namespace Restaurant.Contract.DTOs.Catalog.Brands
{
    public class BrandResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
