using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendTest.Models;

public partial class nutrition
{
    [Key]
    public int nutritionId { get; set; }

    public string name { get; set; } = null!;

    [ForeignKey("nutritionId")]
    [InverseProperty("nutritions")]
    public virtual ICollection<participantsPrivate> participants { get; set; } = new List<participantsPrivate>();
}
