namespace Restaurant.Persistence.DataRecords.Production
{
    internal class RecipeIngredientRecord
    {
        public string ProductName { get; set; } = string.Empty;
        public string IngredientName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string UnitName { get; set; } = string.Empty;
    }
}
