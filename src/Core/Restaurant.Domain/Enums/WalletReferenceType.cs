using System.Text.Json.Serialization;

namespace Restaurant.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum WalletReferenceType
    {
        Invoice,
        Payment,
        Refund,
        AdminAdjustment
    }
}
