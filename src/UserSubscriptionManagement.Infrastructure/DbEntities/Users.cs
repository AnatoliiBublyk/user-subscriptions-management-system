using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserSubscriptionManagement.Infrastructure.DbEntities;

[Table("users")]
public class Users
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [MaxLength(255)]
    [Required]
    [Column("username")]
    public string? Username { get; set; }
    [MaxLength(255)]
    [Required]
    [Column("password_hash")]
    public string? PasswordHash { get; set; }
    [MaxLength(255)]
    [EmailAddress]
    [Required]
    [Column("email")]
    public string? Email { get; set; }
    [ForeignKey(nameof(AdminTypes))]
    [Required]
    [Column("admin_type_id")]
    public int AdminTypeId { get; set; }
    [MaxLength(255)]
    [Required]
    [Column("first_name")]
    public string? FirstName { get; set; }
    [MaxLength(255)]
    [Required]
    [Column("last_name")]
    public string? LastName { get; set; }
    [MaxLength(255)]
    [Column("middle_name")]
    public string MiddleName { get; set; } = string.Empty;
    [MaxLength(255)]
    [Required]
    [Column("address")]
    public string? Address { get; set; }
    [MaxLength(255)]
    [Phone]
    [Required]
    [Column("phone")]
    public string? Phone { get; set; }
    [MaxLength(255)]
    [Required]
    [Column("zip")] 
    public string? Zip { get; set; }
    [Required]
    [Column("is_enabled")]
    public bool IsEnabled { get; set; }

    //Navigation
    public AdminTypes? AdminType { get; set; }
}