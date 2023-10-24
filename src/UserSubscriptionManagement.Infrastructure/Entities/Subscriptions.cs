﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UserSubscriptionManagement.Infrastructure.Entities;

[Table("subscriptions")]
[Index("Key", Name = "UQ__subscrip__DFD83CAFA95BC1A7", IsUnique = true)]
public partial class Subscriptions
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("key")]
    [StringLength(255)]
    [Unicode(false)]
    public string Key { get; set; }

    [Required]
    [Column("title")]
    [StringLength(255)]
    [Unicode(false)]
    public string Title { get; set; }

    [Column("description", TypeName = "text")]
    public string Description { get; set; }

    [Column("duration")]
    public int Duration { get; set; }

    [Column("price")]
    public int Price { get; set; }

    [InverseProperty("Subscription")]
    public virtual ICollection<UserSubscriptions> UserSubscriptions { get; set; } = new List<UserSubscriptions>();
}