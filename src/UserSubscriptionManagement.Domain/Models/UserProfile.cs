using System.ComponentModel.DataAnnotations;

namespace UserSubscriptionManagement.Domain.Models;

public class UserProfile
{
    [Key]
    [Required]
    public int Id { get; set; }

    [MaxLength(255)]
    [EmailAddress]
    [Required]
    public string? Email { get; set; }

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
}