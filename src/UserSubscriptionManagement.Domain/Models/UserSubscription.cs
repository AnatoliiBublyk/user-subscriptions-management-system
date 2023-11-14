using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserSubscriptionManagement.Domain.Models;

public class UserSubscription
{
    [Key]
    public int UserId { get; set; }

    [Key]
    public int SubscriptionId { get; set; }

    [Required]
    public DateOnly StartDate { get; set; }
}