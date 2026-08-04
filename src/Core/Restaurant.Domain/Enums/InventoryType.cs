using System.Text.Json.Serialization;

namespace Restaurant.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum InventoryType
    {
        MadeToOrder,
        StockTracked
    }
}
