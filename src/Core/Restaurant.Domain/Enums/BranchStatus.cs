using System.Text.Json.Serialization;

namespace Restaurant.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BranchStatus
    {
        Active,
        Inactive,
        Closed,
        UnderMaintenance
    }
}
