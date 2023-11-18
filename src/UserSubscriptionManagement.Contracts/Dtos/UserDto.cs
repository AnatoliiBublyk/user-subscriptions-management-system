using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UserSubscriptionManagement.Contracts.Dtos;

public class UserDto
{
    [Key]
    [JsonPropertyName("id")]
    [JsonRequired]
    public int Id { get; set; }

    [MaxLength(255)]
    [JsonPropertyName("username")]
    [JsonRequired]
    public string Username { get; set; } = null!;

    [JsonPropertyName("role")]
    [JsonRequired]
    public string Role { get; set; } = null!;

    [JsonPropertyName("balance")]
    [JsonRequired]
    public decimal Balance { get; set; }

    [JsonPropertyName("is_enabled")]
    [JsonRequired]
    public bool IsEnabled { get; set; }
}