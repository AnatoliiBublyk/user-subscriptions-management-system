using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserSubscriptionManagement.Domain.Models;

public class User
{
    public int Id { get; set; }

    [MaxLength(255)]
    [Required]
    public string? Username { get; set; }

    [MaxLength(255)]
    [Required]
    public string? PasswordHash { get; set; }

    [Required]
    public string? Role { get; set; }

    [Required]
    public decimal Balance { get; set; }

    [Required]
    public bool IsEnabled { get; set; }
}