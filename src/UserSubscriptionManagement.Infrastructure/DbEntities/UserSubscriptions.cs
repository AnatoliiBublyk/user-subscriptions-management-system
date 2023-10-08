using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserSubscriptionManagement.Domain.Models;

namespace UserSubscriptionManagement.Infrastructure.DbEntities;

[Table("user_subscriptions")]
public class UserSubscriptions
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

    //Navigation
    public User? User { get; set; }
    public Subscription? Subscription { get; set; }
}