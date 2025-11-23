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

    [InverseProperty("nutrition")]
    public virtual nutritionsToPrivate? nutritionsToPrivate { get; set; }
}
