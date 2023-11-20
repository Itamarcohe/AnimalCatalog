using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace PetCatalog.Models;

public partial class Comment
{
    [Key]
    public int CommentId { get; set; }

    public string? CommentText { get; set; }

    public int AnimalId { get; set; }

    [Column("commmetTime")]
    public DateTime CommmetTime { get; set; }

    public string? UserName { get; set; }

    [ForeignKey("AnimalId")]
    [InverseProperty("Comments")]
    public virtual Animal Animal { get; set; } = null!;
}
