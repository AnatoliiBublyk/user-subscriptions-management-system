using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserSubscriptionManagement.Domain.Models;

public class Subscription
{
    [Key]
    public int Id { get; set; }

    [MaxLength(255)]
    [Required]
    public string? Key { get; set; }

    [MaxLength(255)]
    [Required]
    public string? Title { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    [Required]
    public int Duration { get; set; }

    [Required]
    public decimal Price { get; set; }
}