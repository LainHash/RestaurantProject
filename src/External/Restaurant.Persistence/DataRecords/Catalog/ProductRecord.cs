namespace Restaurant.Persistence.DataRecords.Catalog
{
    internal class ProductRecord
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public string InventoryType { get; set; } = null!;

        public string BrandName { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string UnitName { get; set; } = null!;
    }
}
