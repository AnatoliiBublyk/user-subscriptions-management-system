using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserSubscriptionManagement.Domain.Models;

[Table("subscriptions")]
public class Subscription
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [MaxLength(255)]
    [Required]
    [Column("key")]
    public string? Key { get; set; }

    [MaxLength(255)]
    [Required]
    [Column("title")]
    public string? Title { get; set; }

    [MaxLength(255)]
    [Column("description")]
    public string? Description { get; set; }
    [Required]
    [Column("duration")]
    public int Duration { get; set; }
    [Required]
    [Column("price")]
    public decimal Price { get; set; }
}