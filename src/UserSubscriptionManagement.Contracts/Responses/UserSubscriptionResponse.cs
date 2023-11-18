using System.Text.Json.Serialization;
using UserSubscriptionManagement.Contracts.Dtos;

namespace UserSubscriptionManagement.Contracts.Responses;

public class UserSubscriptionsResponse
{
    [JsonPropertyName("username")]
    [JsonRequired]
    public string Username { get; set; }

    [JsonPropertyName("subscriptions")]
    [JsonRequired]
    public IEnumerable<SubscriptionDto> Subscriptions { get; set; }
}