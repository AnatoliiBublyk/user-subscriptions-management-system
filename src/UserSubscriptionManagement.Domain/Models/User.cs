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
    [MaxLength(255)]
    [EmailAddress]
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Role { get; set; }
    [MaxLength(255)]
    [Required]
    public string? FirstName { get; set; }
    [MaxLength(255)]
    [Required]
    public string? LastName { get; set; }
    [MaxLength(255)]
    public string MiddleName { get; set; } = string.Empty;
    [MaxLength(255)]
    [Required]
    public string? Address { get; set; }
    [MaxLength(255)]
    [Phone]
    [Required]
    public string? Phone { get; set; }
    [MaxLength(255)]
    [Required]
    public string? Zip { get; set; }
    [Required]
    public bool IsEnabled { get; set; }
}