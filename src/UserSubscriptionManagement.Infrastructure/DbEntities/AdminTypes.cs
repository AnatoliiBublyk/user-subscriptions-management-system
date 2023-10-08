using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserSubscriptionManagement.Infrastructure.DbEntities;

[Table("admin_types")]
public class AdminTypes
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    [Column("admin_type")]
    public string AdminType { get; set; } = null!;
}