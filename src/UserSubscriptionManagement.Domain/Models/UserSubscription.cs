using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserSubscriptionManagement.Domain.Models;

[Table("user_subscriptions")]
public class UserSubscription
{
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }
    [Key]
    [Column("subscription_id")]
    public int SubscriptionId { get; set; }
    [Required]
    [Column("start_date")]
    public DateOnly StartDate { get; set; }
}