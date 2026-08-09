namespace Restaurant.Persistence.DataRecords.Pricing
{
    internal class IngredientPriceRecord
    {
        public decimal UnitPrice { get; set; }

        public string IngredientName { get; set; } = null!;
    }
}
