using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PetCatalog.Models;

[Index("CategoryId", Name = "IX_Animals_CategoryId")]
public partial class Animal
{
    [Key]
    public int AnimalId { get; set; }

    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; } = null!;

    public DateTime Birth { get; set; }

    public string? ImagePath { get; set; }

    public string? Description { get; set; }

    public int CategoryId { get; set; }


    [ForeignKey("CategoryId")]
    [InverseProperty("Animals")]
    public virtual AnimalCategory Category { get; set; } = null!;

    [InverseProperty("Animal")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
