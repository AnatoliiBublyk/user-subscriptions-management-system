using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserSubscriptionManagement.Domain.Models;

public class UserSubscription
{
    [Required]
    public User User { get; set; } = null!;

    [Required]
    public Subscription Subscription { get; set; } = null!;

    [Required]
    public DateTime StartDate { get; set; }
}