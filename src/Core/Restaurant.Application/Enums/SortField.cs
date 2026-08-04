using System.Text.Json.Serialization;

namespace Restaurant.Application.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortField
    {
        CreatedAt,
        Name,
        Price
    }
}
