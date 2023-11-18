using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UserSubscriptionManagement.Contracts.Dtos;

public class UserInfoDto
{
    [Key]
    [JsonPropertyName("id")]
    [JsonRequired]
    public int Id { get; set; }

    [MaxLength(255)]
    [JsonPropertyName("username")]
    [JsonRequired]
    public string Username { get; set; } = null!;

    [MaxLength(255)]
    [JsonPropertyName("password")]
    [JsonRequired]
    public string Password { get; set; } = null!;

    [JsonPropertyName("role")]
    [JsonRequired]
    public string Role { get; set; } = null!;

    [JsonPropertyName("balance")]
    [JsonRequired]
    public decimal Balance { get; set; }

    [JsonPropertyName("is_enabled")]
    [JsonRequired]
    public bool IsEnabled { get; set; }

    [JsonPropertyName("email")]
    [JsonRequired]
    public string? Email { get; set; }

    [JsonPropertyName("first_name")]
    [JsonRequired]
    public string? FirstName { get; set; }

    [JsonPropertyName("last_name")]
    [JsonRequired]
    public string? LastName { get; set; }

    [JsonPropertyName("middle_name")]
    public string MiddleName { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    [JsonRequired]
    public string? Address { get; set; }

    [JsonPropertyName("phone")]
    [JsonRequired]
    public string? Phone { get; set; }

    [JsonPropertyName("zip")]
    [JsonRequired]
    public string? Zip { get; set; }
}