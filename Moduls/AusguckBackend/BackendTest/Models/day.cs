using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendTest.Models;

public partial class day
{
    [Key]
    public int dayId { get; set; }

    public DateOnly date { get; set; }

    [ForeignKey("dayId")]
    [InverseProperty("days")]
    public virtual ICollection<person> people { get; set; } = new List<person>();
}
