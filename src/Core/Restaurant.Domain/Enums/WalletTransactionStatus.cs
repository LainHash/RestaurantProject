using System.Text.Json.Serialization;

namespace Restaurant.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum WalletTransactionStatus
    {
        Pending,
        Processing,
        Succeeded,
        Failed,
        Cancelled
    }
}
