using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PetCatalog.Models;

public partial class AnimalCategory
{
    [Key]
    public int CategoryId { get; set; }

    public string? Name { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();
}
