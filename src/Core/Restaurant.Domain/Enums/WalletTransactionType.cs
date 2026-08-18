using System.Text.Json.Serialization;

namespace Restaurant.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum WalletTransactionType
    {
        Deposit,
        Withdraw,
        Purchase,
        Refund,
        Adjustment,
        Expiration
    }
}
