using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UserSubscriptionManagement.Contracts.Dtos;

public class SubscriptionDto
{
    [Key]
    [JsonPropertyName("id")]
    [JsonRequired]
    public int Id { get; set; }

    [MaxLength(255)]
    [JsonPropertyName("title")]
    [JsonRequired]
    public string Title { get; set; } = null!;

    [MaxLength(255)]
    [JsonPropertyName("key")]
    [JsonRequired]
    public string Key { get; set; } = null!;

    [JsonPropertyName("description")]
    public string Description { get; set; } = null!;

    [JsonPropertyName("duration")]
    [JsonRequired]
    public int Duration { get; set; }
    [JsonPropertyName("price")]
    [JsonRequired]
    public decimal Price { get; set; }
}